using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vido.Model.Model.ViewModel
{
    public class LoadBillModel
    {
        public decimal CheckNo { get; set; }
        public decimal TrnSeq { get; set; }
        public int HaveCoupon { get; set; }
        public int HaveDiscount { get; set; }
    }
}
