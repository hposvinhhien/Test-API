using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vido.Model.Model.Table
{
    [Table("SPOS_APPOINTMENT_DETAIL")]
    public class SPOS_APPOINTMENT_DETAIL
    {
        [Key]
        public Nullable<decimal> AppointmentDetailID { get; set; }
        public Nullable<decimal> SlipAmount { get; set; } = 0;
        public Nullable<decimal> AppointmentID { get; set; }
        public Nullable<long> ItemID { get; set; } = 0;
        public string ItemName { get; set; }
        public Nullable<decimal> EmployeeID { get; set; } = 0;
        public string EmployeeName { get; set; }
        public Nullable<int> Duration { get; set; } = 0;
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public Nullable<int> Status { get; set; } = 0;
        public Nullable<bool> IsPayment { get; set; } = false;
        public Nullable<decimal> Duration_Item { get; set; } = 0;
        public Nullable<int> ExtraDuration { get; set; } = 0;
        public Nullable<decimal> Amount { get; set; } = 0;
        public Nullable<decimal> TaxValue { get; set; } = 0;
        public Nullable<decimal> CRVValue { get; set; } = 0;
        public Nullable<decimal> Qty { get; set; } = 0;
        public Nullable<decimal> OrgAptDetail { get; set; } = 0;
        //public Nullable<bool> IsProduct { get; set; }
        public Nullable<System.DateTime> DateCheckOut { get; set; }
        public Nullable<int> Type { get; set; } = 0;
        public Nullable<bool> IsDelete { get; set; } = false;
        public Nullable<long> ChairCode { get; set; } = 0;
        public Nullable<bool> IsCategory { get; set; } = false;
        public Nullable<bool> isPlus { get; set; } = false;
        public Nullable<int> zIndex { get; set; } = 0;
        public Nullable<bool> IsCheckInTicket { get; set; } = false;
        public Nullable<bool> IsRequestTech { get; set; } = false;
        public Nullable<bool> PushingStatus { get; set; } = false;
        public Nullable<bool> IsModifying { get; set; } = false;
        public bool IsFullTurn { get; set; } = true;
        public Nullable<bool> IsNew { get; set; } = false;
        public Nullable<bool> IsDeleted { get; set; } = false;
        public Nullable<System.DateTime> LastChange { get; set; }
        public Nullable<decimal> TurnID { get; set; } = 0;
        public int RVCNo { get; set; }
        public Nullable<long> IDCombo { get; set; } = 0;
        public Nullable<decimal> PackageID { get; set; } = 0;

        public Nullable<DateTime> LastChangeTime { get; set; } = DateTime.UtcNow;
        public Nullable<bool> IsChangeTime { get; set; } = false;
        // Turn Number 
        public Nullable<decimal> Tax5Num { get; set; } = 0;
        // Deposit Mode Type
        public Nullable<decimal> Tax2Num { get; set; } = 0;
        public string Tax4No { get; set; } = "";
        public Nullable<decimal> Tax4Num { get; set; } = 0;
    }
}
