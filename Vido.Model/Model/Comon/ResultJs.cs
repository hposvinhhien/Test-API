using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vido.Model.Model.Comon
{
    public class ResultJs<T>
    {
        public ResultJs()
        {
            status = 404;
        }
        public int status { get; set; }
        public string message { get; set; }
        public object ExtraData { get; set; }
        public T data { get; set; }
    }
}
