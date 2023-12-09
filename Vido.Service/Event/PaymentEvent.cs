using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vido.Core.Service;
using Vido.Model.Model.Comon;
using Vido.Model.Model.Table;

namespace Vido.Core.Event
{
    public class PaymentEvent
    {
        private static IPaymentService _paymentService;
        private static ITicketService _ticketService;
        private static TicketEvent _ticketEvent;
        public PaymentEvent(IPaymentService paymentService, ITicketService ticketService, TicketEvent ticketEvent)
        {
            _paymentService = paymentService;
            _ticketService = ticketService;
            _ticketEvent = ticketEvent;
        }
        public async Task<ApiResult> discountAllBill(int RVCNo, decimal checkno, decimal vlue, bool IsAmount, int type, decimal EmployeeID, decimal OrgAppointment, bool isCoupon = false)
        {
            var result = new ApiResult();
            bool OnSalon = false; bool OnSalonTech = false; bool OnTech = false;
            switch (type)
            {
                case 3:
                    //Salon
                    OnSalon = true;
                    break;
                case 2:
                    //Salon/Tech
                    OnSalonTech = true;
                    break;
                default:
                    //Tech
                    OnTech = true;
                    break;
            }
            int Resuilt = await _paymentService.P_DNControlDiscountAllBill(RVCNo, checkno, vlue, IsAmount, OnSalon, OnTech, OnSalonTech, EmployeeID, OrgAppointment, isCoupon);
            if (Resuilt == 0)
            {
                result.Status = true;
                result.Message = "Success";
            }
            else if (Resuilt == 1)
            {
                result.Status = false;
                result.Message = "Discount not invalid";
            }
            else if (Resuilt == 3)
            {
                result.Status = false;
                result.Message = "Not Found Bill To Discount";
            }
            return result;
        }

        public static async Task<ApiResult> RemoveTrnTmpAsync(int RVCNo, decimal trnseq, RDTmpTrn tmptrn = null)
        {
            ApiResult result = new ApiResult();
          
            return result;
        }
        public static async Task<bool> VoidTotalDiscount(decimal CheckNo, int RVCNo)
        {
            bool Resuilt = true;
            await _ticketService.VoidTotalDiscount(CheckNo, RVCNo);
            return Resuilt;
        }

        public async Task<ApiResult> AddPaymentCash(decimal SaleId, decimal EmpId, decimal CheckNo, decimal Amount, int RVCNo)
        {
            ApiResult result = new ApiResult();
            try
            {
                if (Amount < 0)
                {
                    result.Status = false;
                    result.Message = "Cannot Pay Less Than 0";
                    return result;
                }
                var cashCode = 200;
                var currentDate = DateTime.UtcNow;
                var lstTmpTrn = _ticketService.select_RDTmpTrn_By_OrgAppointment(CheckNo, RVCNo).ToList();
                var maxOrderNo = lstTmpTrn.Max(x => x.OrderNo);
                var tmpTrn = lstTmpTrn.FirstOrDefault();
                // add new tmptransaction

                var newTmpTrn = _ticketService.CreateEmptyTmpTrn(RVCNo);
                newTmpTrn.EmployeeID = EmpId;
                newTmpTrn.IDSaler = SaleId;
                newTmpTrn.CheckNo = CheckNo;
                newTmpTrn.TrnCode = cashCode;
                newTmpTrn.TrnDesc = "Cash Payment";
                newTmpTrn.BaseTTL = Amount * -1;
                newTmpTrn.AuthorCashier = SaleId;
                newTmpTrn.TrnCode = 200;
                newTmpTrn.OrderNo = maxOrderNo + 1;
                newTmpTrn.OrgCheckNo = tmpTrn.OrgCheckNo;
                newTmpTrn.OrgAppointment = tmpTrn.OrgAppointment;
                newTmpTrn.RefundRef = "0";
                newTmpTrn.ItemCode = "CASH PAY";
                newTmpTrn.Status = tmpTrn.Status;
                newTmpTrn.AppointmentID = tmpTrn.AppointmentID;
                newTmpTrn.RVCNo = RVCNo;

                decimal OldAmt = lstTmpTrn.Sum(x => x.BaseTTL ?? 0);
                decimal trnSeq = _ticketService.Insert_TmpTrn(newTmpTrn);

                await _ticketService.UpdatePayDiscountTicket(RVCNo, CheckNo);

                if (Amount >= OldAmt)
                {

                    Amount = Amount - OldAmt;
                    await _paymentService.P_UpdChangeAmount(RVCNo, CheckNo);
                }

                result.Status = true;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = ex.Message;
                result.ExtraData = ex;

            }
            return result;
        }

    }
}
