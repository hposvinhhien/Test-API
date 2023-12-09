using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vido.Model.Model.Request
{
    public class AddListItemReq
    {
        public string rowid { get; set; }
        public string appID { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string EmployeeID { get; set; }
        public string Price { get; set; }
        public string Dur { get; set; }
        public int? Status { get; set; }
        public int RVCNo { get; set; }
        public string Action { get; set; }
        public decimal Target { get; set; }
        public int? Cost { get; set; }
        public decimal? Turn { get; set; }
        public bool? byPass { get; set; }
        public string DiscountType { get; set; }
        public AddListItemReq[] ChildItem { get; set; }

    }

    public class AddListItemServiceReq
    {
        public decimal AppointmentId { get; set; }
        public long ItemId { get; set; }
        public decimal EmployeeID { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsPayment { get; set; }
        public decimal Amount { get; set; }
        public long TaxValue { get; set; }
        public int Type { get; set; }
        public bool IsRequestTech { get; set; }
        public DateTime LastChange { get; set; }
        public string EmployeeName { get; set; }
        public string ItemName { get; set; }
        public int Status { get; set; }
        public int RVCNo { get; set; }
    }

    public class AddListItemRsp
    {
        public string rowid { get; set; }
    }
}
