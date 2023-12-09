using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Promotion.Application.Services;
using System;
using System.Security.Claims;
using System.Text.Json;
using Vido.Core.Event;
using Vido.Core.Service;
using Vido.Model.Model.Comon;
using Vido.Model.Model.Request;
using Vido.Model.Model.Table;

namespace VidoApi.Controllers
{
    [Route("api/[controller]")]
    public class SaleController : Controller
    {
        private readonly TicketEvent _ticketEvent;
        private readonly PaymentEvent _payEvent;
        private readonly ITicketService _ticketService;
        private readonly ILoginService _loginService;

        public SaleController(PaymentEvent payEvent, TicketEvent ticketEvent, ILoginService loginService, ITicketService ticketService)
        {
            _ticketEvent = ticketEvent;
            _loginService = loginService;
            _ticketService = ticketService;
            _payEvent = payEvent;
        }
        [HttpGet("[action]")]
        public JsonResult LoadCategory()
        {
            try
            {
                var accessToken = Request.Headers[HeaderNames.Authorization];
                var StoreId = _loginService.getStoreIdByToken(accessToken);
                var result = _ticketEvent.LoadCategory(StoreId);
                if(result.status == 200)
                {
                    return Json(new { status = 200, message = "Success", result.data });
                }
                else
                {
                    return Json(new { status = 400, message = "Fail", result.data });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = 500, message = ex.Message, data = ex });
            }
           
        }

        [HttpGet("[action]")]
        public JsonResult LoadItem()
        {
            try
            {
                var accessToken = Request.Headers[HeaderNames.Authorization];
                var StoreId = _loginService.getStoreIdByToken(accessToken);
                var result = _ticketEvent.LoadItem(StoreId);
                if (result.status == 200)
                {
                    return Json(new { status = 200, message = "Success", result.data });
                }
                else
                {
                    return Json(new { status = 400, message = "Fail", result.data });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = 500, message = ex.Message, data = ex });
            }

        }

        [HttpGet("[action]")]
        public JsonResult LoadEmployee()
        {
            try
            {
                var accessToken = Request.Headers[HeaderNames.Authorization];
                var StoreId = _loginService.getStoreIdByToken(accessToken);
                var result = _ticketEvent.LoadEmployee(StoreId);
                if (result.status == 200)
                {
                    return Json(new { status = 200, message = "Success", result.data });
                }
                else
                {
                    return Json(new { status = 400, message = "Fail", result.data });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = 500, message = ex.Message, data = ex });
            }

        }

        [HttpPost("[action]")]
        public async Task<JsonResult> AddListItem(AddListItemReq[] model)
        {
            var result = await _ticketEvent.AddListItem(model);
            return Json(result);
        }

        [HttpPost("[action]")]
        public JsonResult ChangeClient(int RVCNo, decimal CusId, decimal AppointmentID)
        {
            try
            {
                _ticketService.RDChangeClient(RVCNo, CusId, AppointmentID);
                return Json(new { status = 200, message = "Change Client Success" });
               
            }
            catch (Exception ex)
            {
                return Json(new { status = 500, message = ex.Message, data = ex });
            }
        }

        [HttpPut("[action]")]
        public JsonResult PostGiftCard(PostGiftCardReq model)
        {
            try
            {
                string result = _ticketEvent.PostGiftCard(model.litmit, model.Masterstore, model.RVCNo, model.SeriNumber, model.ItemID, model.Amount, model.AptID, model.EmpID, model.SellID, model.OrgAptId);
                if (result == "Success")
                {
                    return Json(new { status = 200, message = "Post GiftCard Success" });
                }
                else
                {
                    return Json(new { status = 500, message = result });
                }

            }
            catch (Exception ex)
            {
                return Json(new { status = 500, message = ex.Message, data = ex });
            }
        }

        [HttpPut("{RVCNo}/[action]")]
        public JsonResult AddOderTip(int RVCNo, decimal CheckNo, decimal EmployeeID, decimal TipAmount)
        {
            try
            {
                _ticketService.AddOderTip(RVCNo, CheckNo, EmployeeID, TipAmount);
                return Json(new { status = 200, message = "success" });
            }
            catch (Exception e)
            {
                return Json(new { status = 500, message = e.Message, Data = e.ToString() });
            }
        }
        [HttpDelete("{RVCNo}/[action]")]
        public JsonResult RemoveOderTip(int RVCNo, decimal CheckNo, decimal TrnSeq)
        {
            try
            {
                _ticketService.RemoveOderTip(RVCNo, CheckNo, TrnSeq);
                return Json(new { status = 200, message = "success" });
            }
            catch (Exception e)
            {
                return Json(new { status = 500, message = e.Message, Data = e.ToString() });
            }
        }

        [HttpGet("{RVCNo}/[action]")]
        public JsonResult GetListTipOnTicket(decimal CheckNo, int RVCNo)
        {
            try
            {
                var result = _ticketEvent.getListTipOnTicket(CheckNo, RVCNo);
                return Json(new { status = 200, message = result });
            }
            catch (Exception e)
            {
                return Json(new { status = 500, message = e.Message, Data = e.ToString() });
            }
        }
        [HttpGet("{RVCNo}/[action]")]
        public JsonResult GetTicketInfo(string token, decimal AptId, int RVCNo)
        {
            try
            {
                var result = _ticketEvent.GetTicketInfo(RVCNo, AptId);
                return Json(new { status = 200, mess = result });
            }
            catch (Exception e)
            {
                return Json(new { status = 500, Mess = e.Message, Data = e.ToString() });
            }
        }
        [HttpGet("[action]")]
        public JsonResult GetBillPreview(int RVCNo, decimal AptId, bool isPay)
        {
            var data = _ticketEvent.getBillPreviewV2(RVCNo, AptId);
            ResultJs<List<ResponseTerminal>> ResponseTerminal = null;
            if (isPay)
            {
                ResponseTerminal = _ticketEvent.getResponseTerminalWithOutSignData(RVCNo, AptId);
            }
            var BillPreview = JsonSerializer.Deserialize<List<Object>>(data.BillPreview ?? "[]");
            var SposDetails = JsonSerializer.Deserialize<List<SPOS_APPOINTMENT_DETAIL>>(data.Spos ?? "[]");
            var Trn = JsonSerializer.Deserialize<List<RDTmpTrn>>(data.Trn ?? "[]");
            return Json(new { BillPreview, ResponseTerminal, SposDetails, Trn });
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> discountAllBill(int RVCNo, decimal checkno, decimal value, bool IsAmount, int type, decimal EmployeeID, decimal OrgAppointment)
        {
            try
            {
                ApiResult result = await _payEvent.discountAllBill(RVCNo, checkno, value, IsAmount, type, EmployeeID, OrgAppointment);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Ok(new { status = 400, Mess = e.Message, Data = e.ToString() });
            }
        }

        [HttpPost("{RVCNo}/[action]")]
        public async Task<JsonResult> CloseBill(CloseBillReq model, int RVCNo)
        {
            try
            {
                ApiResult result = await _ticketEvent.doCloseBill(model.checkno, RVCNo, model.SaleId );
                if (result.Status == true)
                {
                    return Json(new { status = 200, data = result });
                }
                else
                {
                    return Json(new { status = 500, Mess = result.Message, data = result });
                }
            }
            catch (Exception e)
            {
                return Json(new { status = 400, Mess = e.Message, Data = e.ToString() });
            }
        }

        [HttpDelete("{RVCNo}/[action]")]
        public async Task<JsonResult> CancelTicket(int RVCNo, CancelTicketReq model)
        {
            try
            {
                ApiResult result = await _ticketEvent.DoCancel(RVCNo, model.reasons, model.AppointmentID, model.CheckNo, model.CancelBy);
                if (result.Status == true)
                {
                    return Json(new { status = 200 });
                }
                return Json(new { status = 500, Mess = result.Message, Data = result });
            }
            catch (Exception e)
            {
                return Json(new { status = 400, Mess = e.Message, Data = e.ToString() });
            }
        }
        [HttpPost("{RVCNo}/[action]")]
        public async Task<JsonResult> UpdateNoteTicket(string note, int RVCNo, decimal AppointmentID)
        {
            try
            {
                await _ticketService.Update_Note_Ticket(note, RVCNo, AppointmentID);
                return Json(new { status = 200 });
            }
            catch (Exception e)
            {
                return Json(new { status = 400, Mess = e.Message, Data = e.ToString() });
            }
        }

        [HttpDelete("{RVCNo}/[action]")]
        public async Task<JsonResult> DeleteDiscountAllBill(int RVCNo, decimal checkno)
        {
            try
            {
                var result = await PaymentEvent.VoidTotalDiscount(checkno, RVCNo);
                if (result == true)
                {
                    return Json(new { status = 200, data = result });
                }
                else
                {
                    return Json(new { status = 500, Mess = "Void Discount All Bill False", data = result });
                }
            }
            catch (Exception e)
            {
                return Json(new { status = 400, Mess = e.Message, Data = e.ToString() });
            }
        }

        [HttpPut("{RVCNo}/[action]")]
        public async Task<JsonResult> AddPaymentCash(AddPaymentCashReq model, int RVCNo)
        {
            try
            {
                ApiResult result = await _payEvent.AddPaymentCash(model.saleid, model.emmpid, model.checkno, model.cash, RVCNo);
                if (model.payFull == true)
                {
                    await _ticketService.UpdateFullCashPay(RVCNo, model.checkno);
                }
                if (result.Status == true)
                {
                    return Json(new { status = 200, data = result });
                }
                else
                {
                    return Json(new { status = 500, Mess = result.Message, data = result });
                }
            }
            catch (Exception e)
            {
                return Json(new { status = 400, Mess = e.Message, Data = e.ToString() });
            }
        }

    }
}
