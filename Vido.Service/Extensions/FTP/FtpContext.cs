using FluentFTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vido.Model.Model;

namespace Vido.Core.Extensions.FTP
{
    public abstract class FtpContext : IRemoteFileSystemContext
    {
        protected IFtpClient _ftpClient { get; set; }

        public void Connect()
        {
            _ftpClient.Connect();
        }

        public void Disconnect()
        {
            _ftpClient.Disconnect();
        }

        public void Dispose()
        {
            if (_ftpClient != null && !_ftpClient.IsDisposed)
            {
                _ftpClient.Dispose();
            }
        }

        /*actions*/
        public bool FileExists(string filePath)
        {
            return _ftpClient.FileExists(filePath);
        }

        public void DeleteFileIfExists(string filePath)
        {
            if (FileExists(filePath))
            {
                _ftpClient.DeleteFile(filePath);
            }
        }

        public void UploadFile(string localFilePath, string remoteFilePath)
        {
            _ftpClient.UploadFile(localFilePath, remoteFilePath);
        }

        public bool DirectoryExists(string directoryPath)
        {
            return _ftpClient.DirectoryExists(directoryPath);
        }

        public void CreateDirectoryIfNotExists(string directoryPath)
        {
            if (!DirectoryExists(directoryPath))
            {
                _ftpClient.CreateDirectory(directoryPath);
            }
        }

        public void DownloadFile(string localFilePath, string remoteFilePath)
        {
            _ftpClient.DownloadFile(localFilePath, remoteFilePath);
        }

        public bool IsConnected()
        {
            return _ftpClient.IsConnected;
        }

        public void SetWorkingDirectory(string directoryPath)
        {
            _ftpClient.SetWorkingDirectory(directoryPath);
        }

        public void SetRootAsWorkingDirectory()
        {
            SetWorkingDirectory(RemoteSystemSetting.AbsoluteRootDirectory);
        }

        public abstract string ServerDetails();

        public void Rename(string fromPathName, string toPathName)
        {
            if (FileExists(fromPathName))
            {
                _ftpClient.Rename(fromPathName, toPathName);
            }

        }
    }
}
