using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vido.Model.Model.Table
{
    [Table("RDTmpTrn")]
    public class RDTmpTrn
    {
        [Key]
        public decimal TrnSeq { get; set; }
        public Nullable<decimal> NYear { get; set; }
        public Nullable<decimal> NMonth { get; set; }
        public Nullable<decimal> NDay { get; set; }
        public Nullable<decimal> CheckNo { get; set; }
        public Nullable<System.DateTime> TrnTime { get; set; }
        public Nullable<decimal> EmployeeID { get; set; }
        public Nullable<int> TrnCode { get; set; }
        public string ItemCode { get; set; }
        public string TrnDesc { get; set; }
        public Nullable<decimal> ItemPrice { get; set; }
        public Nullable<decimal> BaseSub { get; set; }
        public Nullable<decimal> BaseSrc { get; set; }
        public Nullable<decimal> BaseTax { get; set; }
        public Nullable<decimal> BaseTTL { get; set; }
        public Nullable<long> BaseSTax { get; set; }
        public Nullable<int> SrcNo { get; set; }
        public Nullable<int> STaxNo { get; set; }
        public Nullable<decimal> TaxNo { get; set; }
        public Nullable<decimal> TrnQty { get; set; }
        public Nullable<int> RVC { get; set; }
        public Nullable<bool> NewTrn { get; set; }
        public Nullable<decimal> AuthorCashier { get; set; }
        public Nullable<byte> Status { get; set; }
        public Nullable<int> CategoryCode { get; set; }
        public Nullable<decimal> AppointmentID { get; set; }
        public Nullable<int> Split { get; set; }
        public Nullable<int> OrderNo { get; set; }
        public Nullable<int> ICost { get; set; }
        public Nullable<decimal> DscRef { get; set; }
        public Nullable<decimal> ItemDsc { get; set; }
        public Nullable<decimal> OrgCheckNo { get; set; }
        public Nullable<decimal> OrgAppointment { get; set; }
        public Nullable<decimal> AppointmentDetailID { get; set; }
        public string RefundRef { get; set; }
        public Nullable<decimal> OrgTrnSeq { get; set; }
        public Nullable<decimal> TurnID { get; set; }
        public Nullable<decimal> ProductCharge { get; set; }
        public Nullable<decimal> SliptAmount { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<bool> IsResetDuration { get; set; }
        public int RVCNo { get; set; }
        public Nullable<DateTime> CloseDate { get; set; }
        public Nullable<long> IDCombo { get; set; }
        public Nullable<decimal> IDSaler { get; set; }
        public Nullable<decimal> ExtraRef { get; set; }
        public Nullable<decimal> ExtraDesc { get; set; }
        public string NoteDesc { get; set; }
        public string EmployeeName { get; set; }
        public string ItemName { get; set; }
    }
}
