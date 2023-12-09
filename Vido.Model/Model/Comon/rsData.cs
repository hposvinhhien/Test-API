using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vido.Model.Model.Comon
{
    public class rsData
    {
        public rsData()
        {
            status = 200;
            error_code = 0;
            error_message = "Successful";
            IsTipExtend = false;
            //Successful = true;
            extended_data = new Dictionary<string, object>();
        }

        public Dictionary<string, object> extended_data { get; set; }
        public int error_code { get; set; }
        public int status { get; set; }
        public object data { get; set; }
        public string error_message { get; set; }
        public bool IsTipExtend { get; set; }
    }
}
