using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vido.Model.Model.Table
{
    [Table("POS_CATEGORIES")]
    public partial class Category
    {
        [Key]
        public int CategoryID { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public bool isActive { get; set; }
        public int? zIndex { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string lastestUpdate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? Type { get; set; }
        public int? ParentID { get; set; }
        public string Color { get; set; }
        public string ImageFileName { get; set; }
        public string Description { get; set; }
        public string RVCNo { get; set; }
        public bool IsShowOB { get; set; }
        public bool? IsService { get; set; }
        public bool? IsGiftcard { get; set; }
        public bool? IsShowOnCheckInApp { get; set; }
    }
}
