using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vido.Model.Model.Comon
{
    public class PosAppSetting
    {
        private static IConfigurationSection _configuration;
        public static void Configure(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }

        public static string UrlPortal => _configuration["UrlPortal"];
        public static string ShortLink => _configuration["ShortLink"];
        public static string ImagePath => _configuration["ImagePath"];
    }
}
