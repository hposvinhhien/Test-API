using Pos.Application.Extensions.Helper;
using Pos.Model.Model.Table;
using Promotion.Application.Extensions;
using Promotion.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vido.Model.Model.Proc;
using Vido.Model.Model.Table;
using Vido.Model.Model.ViewModel;

namespace Vido.Core.Service
{
    public interface IPaymentService : IEntityService<RDStore>
    {
        Task P_UpdChangeAmount(int RVCNo, decimal CheckNo);
        Task<int> P_DNControlDiscountAllBill(int RVCNo, decimal checkno, decimal vlue, bool IsAmount, bool onSalon, bool onTech, bool onSalonTech, decimal EmployeeID, decimal OrgAppointment, bool isCoupon);
    }
    public class PaymentService : POSEntityService<RDStore>, IPaymentService
    {
        public async Task<int> P_DNControlDiscountAllBill(int RVCNo, decimal checkno, decimal vlue, bool IsAmount, bool onSalon, bool onTech, bool onSalonTech, decimal EmployeeID, decimal OrgAppointment, bool isCoupon)
        {
            string sql = $@"exec P_DNControlDiscountAllBill {RVCNo} ,{EmployeeID},{checkno},{vlue},{IsAmount},{OrgAppointment},{onSalon},{onSalonTech},{onTech},{isCoupon} ";
            return await _connection.AutoConnect().SqlFirstOrDefaultAsync<int>(sql);
        }
        public async Task P_UpdChangeAmount(int RVCNo, decimal CheckNo)
        {
            string sql = $@"exec P_UpdChangeAmount {CheckNo},{RVCNo}";
            await _connection.AutoConnect().SqlExecuteAsync(sql);
        }
    }
}
