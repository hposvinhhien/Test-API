using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DUDU.Models.Items
{
    public class ItemAssignTech:Item
    {
        public string CategoryName { get; set; }
        public bool? IsAssigned { get; set; }
        public bool? IsCommissionFixed { get; set; }
        public bool? IsPercentageCommissionFixed { get; set; }
        public decimal? CommissionFixedAmount { get; set; }
    }
}
