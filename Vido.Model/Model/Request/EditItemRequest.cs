using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DUDU.Models.Items
{
    public class EditItemRequest
    {
        public long ItemID { get; set; }
        public long CategoryID { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public decimal Tax { get; set; }
        public decimal CashDiscount { get; set; }
        public string ListStaff { get; set; }
        public string ColorCode { get; set; }
        public decimal TurnCount { get; set; }
        public bool NotCountTurn { get; set; }
        public bool ShowOnCheckInApp { get; set; }
        public bool ShowOnBookOnline { get; set; }
        public decimal ProductChargeAmount { get; set; }
        public bool IsProductChargePercentage { get; set; }
        public decimal Commission { get; set; }
    }
}
