using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NetVips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vido.Core.Extensions.FTP;
using Vido.Model.Model.Comon;

namespace Vido.Core.Extensions.Provider
{
    public interface IFileProvider
    {
        string[] GetFileName(string fileNames);
        string[] GetFileName(string[] fileNames);

        Task<ResultJs<string>> UploadFileAsync(IEnumerable<IFormFile> files, string pathRoot, bool isThumbnail = false, bool isCropThumb = false, string userId = "");
        Task<ResultJs<string>> UploadFileAsync(IEnumerable<IFormFile> files, string pathRoot, int with = 256, int? height = null, string userId = "");

        /// <summary>
        /// convert to WebP
        /// </summary>
        /// <param name="file"></param>
        /// <param name="pathRoot"></param>
        /// <returns></returns>
        // Task<string> UploadFileAsync(string file, string pathRoot);
        void Delete(string path, string filename);
        void Delete(string pathFilename);
        Task<string> Thumbnail(string pathFileRead, string pathRoot, int with = 256, int? height = null, NetVips.Enums.Interesting? crop = null);
        Task<string> Thumbnail(System.IO.Stream file, int with = 256, int? height = null, NetVips.Enums.Interesting? crop = null);
        Task<string> Thumbnail(string pathRoot, byte[] buffet, int with = 256, int? height = null, NetVips.Enums.Interesting? crop = null);
        Task<string> SaveImageFromBase64(string pathRoot, string base64, int with = 256, int? height = null, string filename = "", NetVips.Enums.Interesting? crop = null, Enums.Size? size = Enums.Size.Force);
        void Rename(string fromPathName, string toPathName);
    }

    public class FileProvider : IFileProvider
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _configuration;

        public FileProvider(IHostingEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _env = webHostEnvironment;
            _configuration = configuration;
        }

        #region Current

        public async Task<ResultJs<string>> UploadFileAsync(IEnumerable<IFormFile> files, string pathRoot, int width, int? height, string userId = "")
        {
            return await UploadFileAsync(files, pathRoot, width, height, false, false, userId);
        }

        public async Task<ResultJs<string>> UploadFileAsync(IEnumerable<IFormFile> files, string pathRoot, bool isThumbnail = false, bool isCropThumb = false, string userId = "")
        {
            return await UploadFileAsync(files, pathRoot, 0, 0, isThumbnail, isCropThumb, userId);
        }

        public async Task<ResultJs<string>> UploadFileAsync(IEnumerable<IFormFile> files, string pathRoot, int width, int? height, bool isThumbnail = false, bool isCropThumb = false, string userId = "")
        {
            ResultJs<string> result = new ResultJs<string>();
            string path = "";
            string fileNames = "";
            string filename = "";
            long fileSize = 0;
            if (files == null)
            {
                result.status = 200;
                result.data = "";
                return result;
            }

            var lsFiles = files.ToList();
            if (lsFiles.Count > 0)
            {
                string webRootPath = _env.WebRootPath;
                path = Path.Combine(webRootPath, pathRoot);
                string pathThumb = Path.Combine(path, "thumbs");
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                if (!System.IO.Directory.Exists(pathThumb))
                {
                    System.IO.Directory.CreateDirectory(pathThumb);
                }

                foreach (var file in lsFiles)
                {
                    if (file.Length > 0)
                    {
                        byte[] buffer;
                        var tmp = "";
                        if (isThumbnail)
                        {
                            var fileName = Guid.NewGuid().ToString() + "." + file.FileName.Split('.')[1];
                            var saveToPath = Path.Combine(path, fileName);
                            using (FileStream stream = new FileStream(saveToPath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }

                            tmp = await Thumbnail(saveToPath, pathThumb, 72, 72, crop: isCropThumb ? NetVips.Enums.Interesting.Attention : null);
                            try
                            {
                                System.IO.File.Delete(saveToPath);
                            }
                            catch { }

                            fileSize += file.Length;
                            fileNames += tmp + ";";
                        }
                        else
                        {
                            string contentType = file.FileName.Split('.')[1];
                            var fileName = Guid.NewGuid().ToString() + "." + contentType;
                            var saveToPath = Path.Combine(path, fileName);

                            using (FileStream stream = new FileStream(saveToPath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }
                            if (width == 0)
                            {
                                using (Image image = Image.NewFromFile(saveToPath, true))
                                {
                                    width = image.Width;
                                    height = image.Height;
                                }
                            }
                            //System.Drawing.Image image = System.Drawing.Image.FromFile(saveToPath);

                            //int intWidth = image.Width;
                            //int intHeight = image.Height;
                            //image.Dispose();

                            tmp = await Thumbnail(saveToPath, path, width, height, crop: isCropThumb ? NetVips.Enums.Interesting.Attention : null);

                            try
                            {
                                System.IO.File.Delete(saveToPath);
                            }
                            catch (Exception ex)
                            { }

                            fileNames += tmp + ";";
                        }

                        if (!string.IsNullOrWhiteSpace(tmp))
                        {
                            /// FTP
                            IRemoteFileSystemContext remote = new FtpRemoteFileSystem();
                            remote.Connect();
                            bool isconnected = remote.IsConnected();
                            if (isconnected)
                            {

                                remote.SetRootAsWorkingDirectory();
                                remote.CreateDirectoryIfNotExists(isThumbnail ? Path.Combine(pathRoot, "thumbs") : pathRoot);

                                remote.UploadFile(Path.Combine((isThumbnail ? pathThumb : path), tmp),
                                    Path.Combine((isThumbnail ? Path.Combine(pathRoot, "thumbs") : pathRoot), tmp));
                                remote.Disconnect();
                                remote.Dispose();
                            }
                            ///

                        }

                        try
                        {
                            System.IO.File.Delete(Path.Combine(_env.WebRootPath, pathRoot, tmp));
                        }
                        catch (Exception)
                        {

                        }

                    }

                    fileNames = fileNames.TrimEnd(';');

                    result.status = 200;
                    result.message = "Lưu files thành công";
                    result.data = fileNames;
                }
            }

            return result;
        }
        #endregion

        //public void Delete(string fileOld, string fileNew, string pathRoot)
        //{
        //    string[] arrIo = (fileOld ?? "").Split(";");
        //    string[] arrIn = (fileNew ?? "").Split(";");

        //    string delete = string.Join(";", arrIo.Where(x => !arrIn.Any(y => y == x)).ToArray()) ?? "";
        //    Delete(delete, pathRoot);
        //}

        public void Delete(string path, string filenames)
        {
            var file = Path.Combine(path, filenames);
            Delete(file);
        }

        public void Delete(string pathFilename)
        {
            try
            {
                var file = pathFilename;
                IRemoteFileSystemContext remote = new FtpRemoteFileSystem();
                remote.Connect();

                bool isconnected = remote.IsConnected();
                if (isconnected)
                {
                    remote.SetRootAsWorkingDirectory();
                    remote.DeleteFileIfExists(file);
                    remote.Disconnect();
                    remote.Dispose();
                }

                //if (System.IO.File.Exists(file))
                //{
                //    System.IO.File.Delete(file);
                //}
            }
            catch (Exception ex)
            {
            }
        }

        public string[] GetFileName(string fileNames)
        {
            if (fileNames != null)
            {
                return GetFileName(fileNames.Split(";"));
            }
            else
            {
                return null;
            }
        }
        public string[] GetFileName(string[] fileNames)
        {
            if (fileNames != null)
            {
                return fileNames.Select(x => x.Split('/').LastOrDefault() ?? "").ToArray() ?? null;
            }
            else
            {
                return null;
            }
        }

        //public async Task WriteLog(string Message)
        //{
        //    ServerFTP ftp = _txngContext
        //         .Find<ServerFTP>($@"CompanyID = '' and isnull(IsUsed,0)=1");
        //    if (ftp != null)
        //    {
        //        IRemoteFileSystemContext remote = new FtpProvider(new RemoteSystemSetting(ftp));
        //        remote.Connect();
        //        remote.SetRootAsWorkingDirectory();

        //        byte[] buffer = Encoding.ASCII.GetBytes(Message);

        //        string path = "/Logs";
        //        remote.CreateDirectoryIfNotExists(path);

        //        path = path + "/Review_" + DateTime.UtcNow.Date.ToString("YYYY-MM-dd-HH-mm-fff") + ".txt";

        //        var tmp = await remote.UploadAsync(buffer, path);
        //    }
        //}

        public async Task<string> Thumbnail(string pathFileRead, string pathRoot, int with = 256, int? height = null, NetVips.Enums.Interesting? crop = null)
        {
            string fileName = Path.Combine(pathRoot, Guid.NewGuid() + ".webp");
            try
            {
                using Image image = Image.Thumbnail(pathFileRead, with, height, size: Enums.Size.Force, crop: crop);
                image.WriteToFile(fileName);
                image.Dispose();
            }
            catch (Exception ex)
            {
            }
            return fileName.Split('/').LastOrDefault()?.Split('\\').LastOrDefault();
        }

        /// <summary>
        /// not working
        /// </summary>
        /// <param name="buffet"></param>
        /// <param name="with"></param>
        /// <param name="height"></param>
        /// <param name="crop"></param>
        /// <returns></returns>
        public async Task<string> Thumbnail(string pathRoot, byte[] buffet, int with = 256, int? height = null, NetVips.Enums.Interesting? crop = null)
        {
            string fileName = System.IO.Path.Combine(pathRoot, Guid.NewGuid() + ".webp");

            using Image image = Image.ThumbnailBuffer(buffet, with, height: height, crop: crop);
            image.WriteToFile(fileName);
            image.Dispose();

            return fileName;
        }

        /// <summary>
        /// not working
        /// </summary>
        /// <param name="file"></param>
        /// <param name="with"></param>
        /// <param name="height"></param>
        /// <param name="crop"></param>
        /// <returns></returns>
        public async Task<string> Thumbnail(System.IO.Stream file, int with = 256, int? height = null, NetVips.Enums.Interesting? crop = null)
        {
            using Image image = Image.ThumbnailStream(file, with, height: height, crop: crop);
            image.WriteToFile("./wwwroot/" + Guid.NewGuid() + ".webp");
            image.Dispose();
            return "qwer";
        }

        public async Task<string> SaveImageFromBase64(string pathRoot, string base64, int with = 256, int? height = null, string filename = "", NetVips.Enums.Interesting? crop = null, Enums.Size? size = Enums.Size.Force)
        {
            string imageName = (filename == "" ? Guid.NewGuid() : filename) + ".webp";
            string foldPath = Path.Combine(_env.WebRootPath, "uploads");
            string fileName = Path.Combine(_env.WebRootPath, "uploads", imageName);
            string fileNameFtp = Path.Combine(pathRoot, imageName);

            byte[] bytes = Convert.FromBase64String(base64.Substring(base64.Split(',')[0].Length + 1));

            if (!Directory.Exists(foldPath))
            {
                Directory.CreateDirectory(foldPath);
            }

            using Image image = Image.ThumbnailBuffer(bytes, with, height: height, size: Enums.Size.Force, crop: crop);
            image.WriteToFile(fileName);
            image.Dispose();

            /// FTP
            IRemoteFileSystemContext remote = new FtpRemoteFileSystem();
            remote.Connect();
            bool isconnected = remote.IsConnected();
            if (isconnected)
            {
                remote.SetRootAsWorkingDirectory();
                remote.CreateDirectoryIfNotExists(pathRoot);

                remote.UploadFile(fileName, fileNameFtp);
                remote.Disconnect();
                remote.Dispose();
            }
            ///
            System.IO.File.Delete(fileName);
            return imageName;
        }

        public void Rename(string fromPathName, string toPathName)
        {
            IRemoteFileSystemContext remote = new FtpRemoteFileSystem();
            remote.Connect();
            bool isconnected = remote.IsConnected();
            if (isconnected)
            {
                remote.SetRootAsWorkingDirectory();

                remote.Rename(fromPathName, toPathName);
                remote.Disconnect();
                remote.Dispose();
            }
        }
    }

}
