using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DUDU.Models.Items
{
    public class Item
    {
        public long ItemID { get; set; }
        public string ItemName { get; set; }
        public long CategoryID { get; set; }
        public decimal BasePrice { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public decimal Tax { get; set; }
        public decimal CashDiscount { get; set; }
        public bool IsService { get; set; }
        public string ColorCode { get; set; } 
        public decimal? SetTurn { get; set; }
        public bool? IsNotCountTurn { get; set; }
        public bool? IsShowOnCheckInApp { get; set; }
        public bool? IsShowOB { get; set; }
        public decimal? Cost { get; set; }
        public bool? IsPercentServiceCharge { get; set; }
        public decimal? Commission { get; set; }
    }
}
