using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Vido.Model.Model.ViewModel
{
    public class CustomerRequest
    {
        public int Begin { get; set; } = 0;
        public int RecordReturn { get; set; } = 50;
        public string InfoSearch { get; set; } = "";
        public int Sort { get; set; } = 0;
    }
    public class CustomerDto
    {
        public decimal CustomerId { get; set; }
        public decimal? RCPCustomer { get; set; } = 0;
        public string CustomerCode { get; set; }

        // [Required(ErrorMessage = "First Name Required")]
        public string FirstName { get; set; }
        public string LastName { get; set; } = "";
        public Nullable<System.DateTime> Birthday { get; set; }
        public string ContactPhone { get; set; } = "";
        public string WorkPhone { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public string FullName { get; set; } = "";
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public decimal? TotalSpentByYear { get; set; }

        public string Country { get; set; }
        public string ImageFileName { get; set; }
        public Nullable<DateTime> JoinDate { get; set; }

        [JsonIgnore]
        public string PasswordLoginWeb { get; set; }
        public Nullable<int> VisitCount { get; set; }
        public Nullable<System.DateTime> FristVist { get; set; }
        public string FirstVistFormat { get; set; }
        public Nullable<System.DateTime> LastVisit { get; set; }
        public string LastVisitFormat { get; set; }
        public string FavouritePolish { get; set; }
        public string FavouriteTechs1 { get; set; }
        public string FavouriteTechs2 { get; set; }
        public string FavouriteTechs3 { get; set; }
        public string FavouriteTech => String.Join(", ", (new[] { FavouriteTechs1, FavouriteTechs2, FavouriteTechs3 }).Where(s => !string.IsNullOrEmpty(s)));
        public string NotesApp { get; set; }

        public string Coupon { get; set; }

        public decimal? RewardsPoint { get; set; }

        public Nullable<int> Rating { get; set; }
        public string MemberStatus { get; set; }
        public Nullable<bool> IsKid { get; set; }
        public Nullable<bool> IsChild { get; set; }
        public Nullable<System.DateTime> RatingDate { get; set; }
        public Nullable<System.DateTime> RewardsMember { get; set; }
        public Nullable<bool> IsClientVerify { get; set; }
        public string MemberID { get; set; }
        public string Verification { get; set; }
        public bool? IsVerifyPhoneWithMango { get; set; }
        public bool? IsChangePhoneWhenReward { get; set; }

        public bool? IsChangePhone { get; set; } = false;

        public int? VisitCountByYear { get; set; }
        public Nullable<bool> IsBlackList { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<bool> AtRisk { get; set; }
        public Nullable<bool> IsVip { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public string CustomerType { get; set; }
        [NotMapped]
        public decimal? Relationship { get; set; }
        [NotMapped]
        public string RelationshipName { get; set; }
        public bool? isCard { get; set; }
        public bool? IsMember { get; set; }
        public bool? NotReceiveSMS { get; set; }
        public bool? NotReceiveEmail { get; set; }
        public string SearchName { get; set; }
        public bool? Sex { get; set; }
        public string PortalCode { get; set; } = "+1";
        public int? VisitTotal { get; set; } = 0;
        public int? TotalVisit { get; set; } = 0;
        public int? CompletedTotal { get; set; } = 0;
        public int? CanceledTotal { get; set; } = 0;
        public int? VoidedTotal { get; set; } = 0;
        public int? NoshowTotal { get; set; } = 0;
        public int? ReviewTotal { get; set; } = 0;
        public string ImageFull { get; set; } = "";
        public string SizeImage { get; set; } = "";

        public string ProviderName { get; set; } = "";
        public string ProviderSubjectId { get; set; } = "";
        public string Redeem { get; set; } = "";
    }
}
