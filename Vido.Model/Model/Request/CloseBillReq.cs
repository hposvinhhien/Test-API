using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vido.Model.Model.Request
{
    public class CloseBillReq
    {
        public decimal SaleId { get; set; }
        public decimal checkno { get; set; }
        public int timePending { get; set; }
        public int status { get; set; }
        public int RVCNo { get; set; }
    }
}
