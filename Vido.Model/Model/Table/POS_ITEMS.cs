using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vido.Model.Model.Table
{
    public partial class POS_ITEMS
    {
        public long ItemID { get; set; }
        public Nullable<long> CategoryID { get; set; }
        public string ItemCode { get; set; }
        public string BarCode { get; set; }
        public string ItemName { get; set; }
        //public byte[] Image { get; set; }
        public Nullable<decimal> BasePrice { get; set; }
        public string Note { get; set; }
        public Nullable<bool> IsActive { get; set; }

        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string LastestUpdate { get; set; }
        public Nullable<int> Duration { get; set; }
        //public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public Nullable<System.DateTime> LastChange { get; set; }
        public string ImageFileName { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> SetTurn { get; set; }
        public Nullable<bool> IsService { get; set; }

        public Nullable<bool> IsGiftCard { get; set; }
        public Nullable<bool> IsRetail { get; set; }
        public Nullable<bool> IsCopon { get; set; }
        public Nullable<bool> IsVoucher { get; set; }
        public Nullable<bool> CustomerPrice { get; set; }
        public int RVCNo { get; set; }
        public Nullable<bool> IsFee { get; set; }
        public Nullable<int> ItemColumns { get; set; }
        public Nullable<int> ComboStype { get; set; }
        public Nullable<bool> IsShowOnCheckInApp { get; set; }
        public Nullable<bool> IsCombo { get; set; }
        public Nullable<bool> IsPackage { get; set; }
        public Nullable<bool> isPlus { get; set; }
        public bool? IsAllowCommission { get; set; }
        public bool? IsPercentServiceCharge { get; set; }
        public bool? IsFree { get; set; }
        public bool? IsShowOB { get; set; }
        public bool? IsNotCountTurn { get; set; }

        //[NotMapped]
        //public string GroupTech { get; set; }
        //[NotMapped]
        //public string NoGroupTech { get; set; }
    }
}
