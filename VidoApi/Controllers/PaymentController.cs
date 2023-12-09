using Microsoft.AspNetCore.Mvc;
using Promotion.Application.Services;
using Vido.Core.Event;
using Vido.Core.Service;

namespace VidoApi.Controllers
{
    public class PaymentController : Controller
    {
        private readonly TicketEvent _ticketEvent;
        private readonly ITicketService _ticketService;
        private readonly ILoginService _loginService; 
        public PaymentController(TicketEvent ticketEvent, ILoginService loginService, ITicketService ticketService)
        {
            _ticketEvent = ticketEvent;
            _loginService = loginService;
            _ticketService = ticketService;
        }
        [HttpGet("{rvcNo}/[action]")]
        public JsonResult LoadLstOPaymentAvailable(string token, int rvcNo)
        {
            try
            {
                var result = _ticketService.LoadLstOPaymentAvailable(rvcNo);
                return Json(new { status = 200, data = result });
            }
            catch (Exception e)
            {
                return Json(new { status = 500, mess = e.Message, Data = e.ToString() });
            }
        }
        [HttpPut("{rvcNo}/[action]")]
        public JsonResult addOPayment(int RVCNo, int trncode, decimal value, decimal checkno, string desc, decimal EmpID, decimal SaleId)
        {
            try
            {
                _ticketService.addOPayment(RVCNo, trncode, value, checkno, desc, EmpID, SaleId);
                return Json(new { status = 200, mess = "Success" });
            }
            catch (Exception e)
            {
                return Json(new { status = 500, mess = e.Message, Data = e.ToString() });
            }
        }
    }
}
