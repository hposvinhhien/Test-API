using FluentFTP;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vido.Core.Extensions.Helper;
using Vido.Model.Model;

namespace Vido.Core.Extensions.FTP
{
    public class FtpRemoteFileSystem : FtpContext
    {
        private string _serverDetails;

        public FtpRemoteFileSystem()
        {
            _serverDetails = FtpHelper.ServerDetails
                 (RemoteSystemSetting.Host,
                  RemoteSystemSetting.Port.ToString(),
                  RemoteSystemSetting.UserName,
                  RemoteSystemSetting.Type);

            _ftpClient = new FtpClient(RemoteSystemSetting.Host);
            _ftpClient.Credentials = new System.Net.NetworkCredential
                                         (RemoteSystemSetting.UserName, RemoteSystemSetting.Password);
            _ftpClient.Port = RemoteSystemSetting.Port;
        }

        public override string ServerDetails()
        {
            return _serverDetails;
        }
    }
}
