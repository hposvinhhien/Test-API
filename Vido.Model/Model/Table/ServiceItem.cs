using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vido.Model.Model.Table
{
    [Table("POS_ITEMS")]
    public partial class ServiceItem
    {
        [Key]
        public long ItemID { get; set; }
        public long CategoryID { get; set; }
        public string ItemCode { get; set; }
        public string BarCode { get; set; }
        public string ItemName { get; set; }
        public decimal? BasePrice { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ZIndex { get; set; }
        public int? Duration { get; set; }
        public int? Quantity { get; set; }
        public decimal? Cost { get; set; }
        public int? LeftQuantity { get; set; }
        public bool? isPlus { get; set; }
        public string Description { get; set; }
    }
}
