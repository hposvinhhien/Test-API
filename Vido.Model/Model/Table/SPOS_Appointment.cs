using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vido.Model.Model.Table
{
    [Table("SPOS_APPOINTMENT")]
    public class SPOS_Appointment
    {
        [Key]
        [Required]
        public decimal AppointmentID { get; set; }
        public string AppointmentSubject { get; set; }
        public Nullable<decimal> CustomerID { get; set; } = 0;
        public Nullable<DateTime> ServiceDate { get; set; }
        public Nullable<DateTime> StartTime { get; set; }
        public Nullable<DateTime> EndTime { get; set; }
        public Nullable<decimal> EmployeeID { get; set; } = 0;
        public Nullable<decimal> AptEmployeeID { get; set; } = 0;
        public Nullable<int> AppointmentStatusID { get; set; } = 1;
        public Nullable<long> IsPackage { get; set; }
        public Nullable<bool> WaitingList { get; set; } = false;
        public Nullable<decimal> RecurringAppointmentID { get; set; } = 0;
        public Nullable<decimal> TotalAmount { get; set; } = 0;
        public Nullable<decimal> TotalPaid { get; set; } = 0;
        public Nullable<bool> IsWorking { get; set; } = false;
        public Nullable<DateTime> CheckinTime { get; set; }
        public Nullable<bool> IsProduct { get; set; } = false;
        public Nullable<bool> IsReminder { get; set; } = false;
        public string BarcodeTicket { get; set; }
        public Nullable<bool> IsGroup { get; set; } = false;
        public string ReferenceAppointmentID { get; set; }
        public Nullable<decimal> OriginalAppointmentID { get; set; }
        public Nullable<int> BookType { get; set; }
        public Nullable<decimal> DepositAmount { get; set; } = 0;
        public Nullable<bool> PushingStatus { get; set; } = false;
        public Nullable<bool> IsModifying { get; set; } = false;
        public Nullable<bool> IsNew { get; set; } = false;
        public Nullable<bool> IsDeleted { get; set; } = false;
        public Nullable<DateTime> LastChange { get; set; }
        public string DeleteReason { get; set; }
        public Nullable<bool> IsDelete { get; set; } = false;
        public string CrearteBy { get; set; }
        public string LastChangeBy { get; set; }
        public Nullable<DateTime> CreateDate { get; set; }
        public Nullable<DateTime> OutService { get; set; }
        public Nullable<DateTime> InService { get; set; }
        public Nullable<bool> IsTagFriend { get; set; } = false;
        public Nullable<bool> IsAddGuest { get; set; } = false;
        public Nullable<bool> IsParty { get; set; } = false;
        public Nullable<decimal> IDParty { get; set; } = 0;
        public Nullable<decimal> CheckNo { get; set; } = 0;
        public Nullable<bool> AptMain { get; set; } = false;
        public Nullable<bool> NeedChange { get; set; } = false;
        public Nullable<bool> IsRequest { get; set; } = false;
        public Nullable<System.DateTime> AptStartTime { get; set; }
        public Nullable<System.DateTime> AptEndTime { get; set; }
        public Nullable<bool> AptSalon { get; set; } = false;
        public Nullable<decimal> OrgCheckNo { get; set; }
        public string AptComment { get; set; }
        public int RVCNo { get; set; }
        public bool IsFullTurn { get; set; } = true;
        public Nullable<int> IndexNum { get; set; } = 0;
        public Nullable<bool> IsCreateAndChange { get; set; } = false;
        public Nullable<bool> IsBookOnline { get; set; } = false;
        public Nullable<bool> IsConfirmOB { get; set; } = false;
        public Nullable<bool> IsCountTurn { get; set; } = false;
        public string EmpsMarkAsRead { get; set; }
        public bool? IsMarkAsRead { get; set; } = false;
        public string ClientName { get; set; }
        public string ClientPhone { get; set; }
        public Nullable<bool> IsVip { get; set; } = false;

        public int? TotalDuration { get; set; } = 0;
        [NotMapped]
        public List<SPOS_APPOINTMENT_DETAIL> listDetail { get; set; }

    }

}
