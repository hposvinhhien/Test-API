using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vido.Model.Model.Request
{
    public class ItemAssignTechRequest
    {
        public decimal ItemID { get; set; }
        public bool? IsAssigned { get; set; }
        public bool? IsCommissionFixed { get; set; }
        public bool? IsPercentageCommissionFixed { get; set; }
        public decimal? CommissionFixedAmount { get; set; }
    }
}
