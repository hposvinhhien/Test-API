using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vido.Model.Model.Proc
{
    public class LoadCategoryRsp
    {
        public long? CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Image { get; set; }
        public bool? IsActive { get; set; }
        public int? RVCNo { get; set; }
        public string Color { get; set; }
        public string ImageFileName { get; set; }
        public bool? IsDeleted { get; set; }

    }
}
