using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vido.Model.Model.Comon
{
    public class ApiResult
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public Object ExtraData { get; set; }
    }
}
