using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vido.Model.Model.Table;

namespace Vido.Model.Model.Proc
{
    public class EmployeeStructureViewModel : Employee
    {
        public int PaystructureType { get; set; }
        public decimal? HourSalary { get; set; }

        public decimal Salary { get; set; }
        public int? SalaryPeriod { get; set; }
        public int? MinDay { get; set; }
        public int? MinHour { get; set; }
        public int? MinTotalHour { get; set; }
        public bool? NoRequirement { get; set; }

        public decimal? ProductCommission { get; set; }
        public decimal? PayrollCheckPercentage { get; set; }

        public decimal? TipOnCCFee { get; set; }
        public decimal? TipCheckPercentage { get; set; }

        public int? GroupPermissionID { get; set; }
        public string PermissionString { get; set; }

        public string CommissionServiceString { get; set; }

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
        public decimal? ServiceCommission { get; set; }
        public bool? IsFixedServiceCommission { get; set; }
        public bool? IsFixedProductCommission { get; set; }

        //public List<TechCommission> TechCommissions { get; set; }
    }
}
