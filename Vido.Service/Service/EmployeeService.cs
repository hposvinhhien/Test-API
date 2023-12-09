using Promotion.Application.Extensions;
using Promotion.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vido.Model.Model.Comon;
using Vido.Model.Model.Table;
using Vido.Model.Model.Request;
using Vido.Model.Model.Proc;
using Dapper;
using Pos.Application.Extensions.Helper;

namespace Vido.Core.Service
{
    public interface IEmployeeService : IEntityService<Employee>
    {
        bool AddEmployee(int rvcNo, AddEmployeeRequest request, string itemAssignedXML);
        List<Employee> GetListEmployee(int rvcNo);
        EmployeeStructureViewModel GetEmployeeDetail(decimal employeeID, int rvcNo);
        bool EditEmployee(EditEmployeeRequest request, string itemAssignedXML);
        bool DeleteEmployee(decimal employeeID, int rvcNo);
        Employee GetEmployeeByTouchID(int rvcNo, string touchID);
        string GetAccessibilityLst(decimal sEmployeeID, int RVCNo);
        bool CheckTechIsLogin(decimal sEmployeeID, int RVCNo);
        Object GetLstPerIandP(int rvcNo);

    }

    public class EmployeeService : POSEntityService<Employee>, IEmployeeService
    {
        public EmployeeService()
        {
        }
        public EmployeeService(IDbConnection db) : base(db)
        {

        }
        public bool AddEmployee(int rvcNo, AddEmployeeRequest request, string itemAssignedXML)
        {
            try
            {
                string query = $"P_AddEmployee N'{request.FirstName}',N'{request.LastName}',N'{request.TouchID}',N'{request.NickName}',N'{request.Title}'," +
                    $"N'{request.Note}','{request.Email}','{request.Phone}','{request.StartDate}',{(request.Status ? 1 : 0)},{(request.IsTechnician ? 1 : 0)}," +
                    $"{request.ProductCommission ?? 0},'{request.ColorCode}',{rvcNo},'{request.ImageFileName}','{request.SizeImage}'," +
                    $"'{itemAssignedXML}',{request.GroupPermissionID},'{request.PermissionString}'," +
                    $"{request.PaystructureType ?? 0},{request.HourSalary ?? 0},{request.Salary},{request.MinDay},{request.MinHour},{(request.MinTotalHour == null ? "null" : request.MinTotalHour)}," +
                    $"{request.SalaryPeriod ?? 0},{request.NoRequirement ?? false},{request.PayrollCheckPercentage ?? 0},{request.TipOnCCFee ?? 0},{request.TipCheckPercentage ?? 0}," +
                    $"{((request.TakeAppointment ?? false) ? 1 : 0)},{((request.BookOnline ?? false) ? 1 : 0)},{((request.QuickCheckout ?? false) ? 1 : 0)},{((request.HoldCash ?? false) ? 1 : 0)}," +
                    $"{request.TurnBonus ?? 0},{((request.SurchargePercentageChoosed ?? false) ? 1 : 0)},{request.SurchargeMoney ?? 0},{request.SurchargePercentage ?? 0}," +
                    $"{request.SurchargeTypeID ?? 1},{(request.SurchargeMinDay == null ? "null" : request.SurchargeMinDay)},{(request.SurchargeMinHour == null ? "null" : request.SurchargeMinHour)},{(request.SurchargeMinTotalHour == null ? "null" : request.SurchargeMinTotalHour)}," +
                    $"{((request.ProductChargeBaseOnTicket ?? false) ? 1 : 0)},{request.MinTicketAmount ?? 0},{((request.IsProductChargePercentage ?? false) ? 1 : 0)},{request.ProductChargeAmount ?? 0}," +
                    $"{((request.CommissionAndSalary ?? false) ? 1 : 0)},{((request.IsFixedServiceCommission ?? false) ? 1 : 0)},{request.ServiceCommission ?? 0},{((request.IsFixedProductCommission ?? false) ? 1 : 0)}";

                _connection.AutoConnect().SqlExecute(query);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public List<Employee> GetListEmployee(int rvcNo)
        {
            return _connection.AutoConnect().SqlQuery<Employee>($"exec P_GetListEmployee {rvcNo}").ToList();
        }
        public EmployeeStructureViewModel GetEmployeeDetail(decimal employeeID, int rvcNo)
        {
            string query = $"P_GetEmployeeDetail {employeeID},{rvcNo}";
            return _connection.AutoConnect().SqlFirstOrDefault<EmployeeStructureViewModel>(query);
        }
        public string GetAccessibilityLst(decimal sEmployeeID, int RVCNo)
        {
            string query = $@"select AccessibilityAllowList from RDPayStructureEmployee with (nolock) where EmployeeID='{sEmployeeID}' and RVCNo='{RVCNo}'";
            string roleString = _connection.AutoConnect().SqlFirstOrDefault<string>(query);
            return roleString;
        }
        public bool CheckTechIsLogin(decimal sEmployeeID, int RVCNo)
        {
            string query = $@"select top 1 1 from RDTurnManagement  WITH (NOLOCK)   
             where  RVCNo = {RVCNo} and (COALESCE (IsLogIn, convert(bit,0)) = 1) AND (COALESCE (IsLogOut,  convert(bit,0)) = 0)   
             AND  TurnDate <= GETUTCDATE() 
             and EmployeeID ={sEmployeeID}";
            int? roleString = _connection.AutoConnect().SqlFirstOrDefault<int>(query);
            if (roleString == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public Object GetLstPerIandP(int rvcNo)
        {
            string query = $"select EmployeeID, ','+isnull(ServiceIDAllowList,'')+','+isnull(ProductIDAllowList,'')+',' as lstPerIandP from RDPayStructureEmployee with (nolock) where RVCNo='{rvcNo}'";
            return _connection.AutoConnect().SqlQuery<Object>(query);
        }
        public bool EditEmployee(EditEmployeeRequest request, string itemAssignedXML)
        {
            string query = $"P_EditEmployee {request.EmployeeID},N'{request.FirstName}',N'{request.LastName}',N'{request.TouchID}',N'{request.NickName}',N'{request.Title}'," +
                    $"N'{request.Note}','{request.Email}','{request.Phone}','{request.StartDate}',{(request.Status ? 1 : 0)},{(request.IsTechnician ? 1 : 0)}," +
                    $"{request.ProductCommission ?? 0},'{request.ColorCode}',{request.RVCNo},'{request.ImageFileName}','{request.SizeImage}'," +
                    $"'{itemAssignedXML}',{request.GroupPermissionID},'{request.PermissionString}'," +
                    $"{request.PaystructureType ?? 0},{request.HourSalary ?? 0},{request.Salary},{request.MinDay},{request.MinHour},{(request.MinTotalHour == null ? "null" : request.MinTotalHour)}," +
                    $"{request.SalaryPeriod ?? 0},{(request.NoRequirement ?? false ? 1 : 0)},{request.PayrollCheckPercentage ?? 0},{request.TipOnCCFee ?? 0},{request.TipCheckPercentage ?? 0}," +
                    $"{((request.TakeAppointment ?? false) ? 1 : 0)},{((request.BookOnline ?? false) ? 1 : 0)},{((request.QuickCheckout ?? false) ? 1 : 0)},{((request.HoldCash ?? false) ? 1 : 0)}," +
                    $"{request.TurnBonus ?? 0},{((request.SurchargePercentageChoosed ?? false) ? 1 : 0)},{request.SurchargeMoney ?? 0},{request.SurchargePercentage ?? 0}," +
                    $"{request.SurchargeTypeID ?? 1},{(request.SurchargeMinDay == null ? "null" : request.SurchargeMinDay)},{(request.SurchargeMinHour == null ? "null" : request.SurchargeMinHour)},{(request.SurchargeMinTotalHour == null ? "null" : request.SurchargeMinTotalHour)}," +
                    $"{((request.ProductChargeBaseOnTicket ?? false) ? 1 : 0)},{request.MinTicketAmount ?? 0},{((request.IsProductChargePercentage ?? false) ? 1 : 0)},{request.ProductChargeAmount ?? 0}," +
                    $"{((request.CommissionAndSalary ?? false) ? 1 : 0)},{((request.IsFixedServiceCommission ?? false) ? 1 : 0)},{request.ServiceCommission ?? 0},{((request.IsFixedProductCommission ?? false) ? 1 : 0)}";
            _connection.AutoConnect().SqlExecute(query);

            return true;
        }
        public bool DeleteEmployee(decimal employeeID, int rvcNo)
        {
            string query = $"exec P_DeleteEmployee {rvcNo},{employeeID}";
            _connection.AutoConnect().SqlExecute(query);
            return true;
        }
        public Employee GetEmployeeByTouchID(int rvcNo, string touchID)
        {
            return _connection.AutoConnect().SqlFirstOrDefault<Employee>($"exec P_GetListEmployee {rvcNo}");
        }
    }
}
