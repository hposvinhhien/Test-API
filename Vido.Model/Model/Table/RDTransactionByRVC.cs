using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vido.Model.Model.Table
{
    public class RDTransactionByRVC
    {
        public int TrnCode { get; set; }
        public int TrnSubCode { get; set; }
        public string Description { get; set; }
        public Byte? TrnType { get; set; }
        public bool? SystemUse { get; set; }
        public bool? TrnActive { get; set; }
        public int? RVCNo { get; set; }
    }
}
