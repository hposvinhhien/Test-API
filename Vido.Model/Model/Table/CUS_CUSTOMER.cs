using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vido.Model.Model.Table
{
    public partial class CUS_CUSTOMER //: IBaseEntity
    {
        [Key]
        public decimal CustomerID { get; set; }
        //public string CustomerCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MemberID { get; set; }
        public Nullable<System.DateTime> JoinDate { get; set; }
        public Nullable<int> Sex { get; set; }
        public Nullable<bool> IsChild { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public string LastestUpdate { get; set; }
        //public Nullable<bool> IsDeleted { get; set; }
        // public Nullable<long> Versions { get; set; }
        public string ContactPhone { get; set; }
        public string WorkPhone { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public string CustomerName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string SocialSecurity { get; set; }
        //public Nullable<bool> IsFull { get; set; }
        public string CustomerCard { get; set; }
        public string Country { get; set; }
        public string ImageFileName { get; set; }
        public Nullable<System.DateTime> LastChange { get; set; }
        public string Verification { get; set; }
        public Nullable<System.DateTime> DateVerify { get; set; }
        public string PhoneChange { get; set; }
        public bool? IsPhoneChange { get; set; }
        public int RVCNo { get; set; }
        public bool? IsMember { get; set; }
        public bool? NotReceiveSMS { get; set; }
        public bool? NotReceiveEmail { get; set; }
        public string SearchName { get; set; }
        public decimal? Relationship { get; set; }
        public string RelationshipName { get; set; }
        public string PasswordLoginWeb { get; set; }
        public bool? isCard { get; set; }

        public string CustomerType { get; set; }
        public bool? IsBlackList { get; set; }
        //public bool? NotReceiveSMS { get; set; }
        //public bool? NotReceiveEmail { get; set; }
    }
}
