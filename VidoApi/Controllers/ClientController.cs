using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Vido.Core.Event;
using Vido.Core.Service;
using Vido.Model.Model.Request;
using Vido.Model.Model.Table;
using Vido.Model.Model.ViewModel;

namespace VidoApi.Controllers
{
    [Route("api/[controller]")]
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }
        [HttpPut("[action]")]
        public JsonResult RegisterClient(FindAndRegCustomerModel model)
        {
            try
            {

                CUS_CUSTOMER result = _clientService.RegisterClient(model);
                if (result != null)
                {
                    return Json(new { status = 200, result });
                }
                else
                {
                    return Json(new { status = 500, mess = "Client Already Exists" });
                }
            }
            catch (Exception e)
            {
                return Json(new { status = 500, mess = e.Message, data = e });
            }

        }


        [HttpGet("{rvcNo}/GetAll")]
        public JsonResult GetAll(int rvcNo, [FromQuery] CustomerRequest model)
        {
            try
            {

                var ClientData = _clientService.GetAll(rvcNo, model).ToList();
                return Json(new { status = 200, ClientData });
            }
            catch (Exception e)
            {
                return Json(new { status = 500, mess = e.Message, data = e });
            }
        }
        [HttpGet("{rvcNo}/Get")]
        public JsonResult Get(int rvcNo, decimal CustomerId)
        {
            try
            {

                var ClientData = _clientService.Get(rvcNo, CustomerId);
                return Json(new { status = 200, ClientData });
            }
            catch (Exception e)
            {
                return Json(new { status = 500, mess = e.Message, data = e });
            }
        }

        [HttpPost("{rvcNo}/Update")]
        public JsonResult Update(CUS_CUSTOMER client)
        {
            try
            {

                var ClientData = _clientService.UpdateClient(client);
                return Json(new { status = 200, ClientData });
            }
            catch (Exception e)
            {
                return Json(new { status = 500, mess = e.Message, data = e });
            }
        }

        [HttpDelete("{rvcNo}/Delete")]
        public async Task<JsonResult> Delete(int rvcNo, decimal CustomerId)
        {
            try
            {
                await _clientService.DeleteClient(rvcNo, CustomerId);
                return Json(new { status = 200, mess = "Delete Success" });
            }
            catch (Exception e)
            {
                return Json(new { status = 500, mess = e.Message, data = e });
            }
        }
    }
}
