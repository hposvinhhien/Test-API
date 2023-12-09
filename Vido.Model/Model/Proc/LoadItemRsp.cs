using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vido.Model.Model.Proc
{
    public class LoadItemRsp
    {
        public long? ItemID { get; set; }
        public long? CategoryID { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public decimal? BasePrice { get; set; }
        public int Duration { get; set; }
        public int? RVCNo { get; set; }
        public string ColorCode { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsShowOnCheckInApp { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
