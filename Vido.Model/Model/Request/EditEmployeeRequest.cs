using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vido.Model.Model.Request
{
    public class EditEmployeeRequest
    {
        public decimal EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TouchID { get; set; }
        public string NickName { get; set; }
        public string Title { get; set; }

        public string Note { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string StartDate { get; set; }
        public bool Status { get; set; }
        public bool IsTechnician { get; set; }

        public string ColorCode { get; set; }

        public string Base64Full { get; set; }
        public int? WidthFull { get; set; }
        public int? HeightFull { get; set; }

        public string Base64Crop { get; set; }
        public string SizeImage { get; set; }
        public string ImageFileName { get; set; }

        public int GroupPermissionID { get; set; }
        public string PermissionString { get; set; }

        public int? PaystructureType { get; set; }
        public decimal? HourSalary { get; set; }

        public decimal? Salary { get; set; }
        public int? SalaryPeriod { get; set; }
        public int? MinDay { get; set; }
        public int? MinHour { get; set; }
        public int? MinTotalHour { get; set; }
        public bool? NoRequirement { get; set; }

        public decimal? ProductCommission { get; set; }
        public decimal? PayrollCheckPercentage { get; set; }

        public decimal? TipOnCCFee { get; set; }
        public decimal? TipCheckPercentage { get; set; }

        public List<ItemAssignTechRequest> ItemAssignedTech { get; set; }

        public int RVCNo { get; set; }

        public bool? TakeAppointment { get; set; }
        public bool? BookOnline { get; set; }
        public bool? QuickCheckout { get; set; }
        public bool? HoldCash { get; set; }

        public decimal? TurnBonus { get; set; }

        public decimal? SurchargeMoney { get; set; }
        public bool? SurchargePercentageChoosed { get; set; }
        public decimal? SurchargePercentage { get; set; }

        public int? SurchargeTypeID { get; set; }
        public int? SurchargeMinDay { get; set; }
        public decimal? SurchargeMinHour { get; set; }
        public decimal? SurchargeMinTotalHour { get; set; }

        public bool? ProductChargeBaseOnTicket { get; set; }
        public decimal? MinTicketAmount { get; set; }
        public bool? IsProductChargePercentage { get; set; }
        public decimal? ProductChargeAmount { get; set; }

        public bool? CommissionAndSalary { get; set; }
        public bool? IsFixedServiceCommission { get; set; }
        public bool? IsFixedProductCommission { get; set; }
        public decimal? ServiceCommission { get; set; }

        public string OldValues { get; set; }
    }
}
