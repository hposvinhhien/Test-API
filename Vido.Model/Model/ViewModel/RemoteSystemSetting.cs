using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vido.Model.Model
{
    public static class RemoteSystemSetting
    {
        private static IConfigurationSection _config;
        public static void Configure(IConfigurationSection config)
        {
            _config = config;
        }

        public static string Host => _config["Host"];
        public static int Port => Convert.ToInt32(_config["Port"]);
        public static string UserName => _config["UserName"];
        public static string Password => _config["Password"];
        public static string Type => _config["Type"];
        public static string AbsoluteRootDirectory => _config["AbsoluteRootDirectory"];
    }
}
