using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vido.Model.Model.Request
{
    public class PostGiftCardReq
    {
        public DateTime? litmit { get; set; }
        public int Masterstore { get; set; }
        public int RVCNo { get; set; }
        public string SeriNumber { get; set; }
        public long ItemID { get; set; }
        public decimal Amount { get; set; }
        public decimal AptID { get; set; }
        public decimal EmpID { get; set; }
        public decimal SellID { get; set; }
        public decimal OrgAptId { get; set; }
    }

    public class PostGiftCardRsp
    {
        public bool status { get; set; }
        public string mess { get; set; }
        public object data { get; set; }
    }
}
