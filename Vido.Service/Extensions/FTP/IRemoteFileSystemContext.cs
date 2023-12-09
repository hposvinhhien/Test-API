using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vido.Core.Extensions.FTP
{
    public interface IFileSystem
    {
        bool FileExists(string filePath);
        bool DirectoryExists(string directoryPath);
        void CreateDirectoryIfNotExists(string directoryPath);
        void DeleteFileIfExists(string filePath);
        void Rename(string fromPathName, string toPathName);

    }
    public interface IRemoteFileSystemContext : IFileSystem, IDisposable
    {
        bool IsConnected();
        void Connect();
        void Disconnect();

        void SetWorkingDirectory(string path);
        void SetRootAsWorkingDirectory();

        void UploadFile(string localFilePath, string remoteFilePath);
        void DownloadFile(string localFilePath, string remoteFilePath);

        string ServerDetails();
    }
}
