using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vido.Model.Model.Table
{
    public class RDGiftCardDetail
    {
        public decimal ID { get; set; }

        public decimal CheckNo { get; set; }

        public decimal IDGirftCard { get; set; }

        public DateTime UseDate { get; set; }

        public decimal Amount { get; set; }

        public Nullable<decimal> CustomerID { get; set; }

        public Nullable<decimal> TrnSeq { get; set; }

        public int RVCNo { get; set; }
        public int? MasterStore { get; set; }
    }
}
