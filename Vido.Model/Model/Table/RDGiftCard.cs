using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vido.Model.Model.Table
{
    public class RDGiftCard
    {
        public decimal ID { get; set; }

        public Nullable<decimal> CheckNo { get; set; }

        public string GirfCode { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? ActiveDate { get; set; }

        public Nullable<decimal> CustomerID { get; set; }

        public Nullable<decimal> OrgCheckNo { get; set; }
        public Nullable<decimal> Amount { get; set; }

        public Nullable<decimal> Balance { get; set; }

        public Nullable<System.DateTime> LastChange { get; set; }

        public Nullable<bool> Limit { get; set; }

        public Nullable<System.DateTime> LimitFrom { get; set; }

        public Nullable<System.DateTime> LimitTo { get; set; }

        public string PortalCodeReceipeint { get; set; }

        public string RecipeintPhone { get; set; }

        public string SenderName { get; set; }

        public string SenderPhone { get; set; }

        public string ReceipeintEmail { get; set; }

        public string Message { get; set; }

        public string ReceipeintName { get; set; }

        public string TemplateID { get; set; }

        public Nullable<decimal> TrnSeq { get; set; }

        public Nullable<bool> IsFOC { get; set; }

        public Nullable<bool> UseGroupOfStores { get; set; }

        public Nullable<int> MasterStore { get; set; }

        public int? RVCNo { get; set; }

    }
    [NotMapped]
    public class RDGiftCardViewModel : RDGiftCard
    {
        public Nullable<decimal> AppointmentID { get; set; }
        public Nullable<DateTime> UseDate { get; set; }
        public Nullable<decimal> AmountUse { get; set; }
        public string RVCName { get; set; }
    }
}
