using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vido.Model.Model.Request
{
    public class AddPaymentCashReq
    {
        public decimal emmpid { get; set; }
        public decimal saleid { get; set; }
        public decimal checkno { get; set; }
        public decimal cash { get; set; }
        public decimal changeAmt { get; set; }
        public bool? payFull { get; set; }
    }
}
