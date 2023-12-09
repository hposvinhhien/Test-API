using Microsoft.AspNetCore.Mvc;
using Vido.Core.Event;
using Vido.Core.Service;

namespace VidoApi.Controllers
{
    [Route("api/[controller]")]
    public class IMSManageController : Controller
    {
        private readonly IMSManageEvent _imsManageEvent;
        private readonly IIMSManageService _imsManageService;
        public IMSManageController(IMSManageEvent imsManageEvent, IIMSManageService imsManageService)
        {
            _imsManageEvent = imsManageEvent;
            _imsManageService = imsManageService;
        }
        [HttpPut("[action]")]
        public JsonResult ResgisStore(string sName, string sPhone, string sEmail, string sPass)
        {
            try
            {
                var results = _imsManageEvent.ResgisStore(sName, sPhone, sEmail, sPass);

                return Json(new { results.status, message = results.error_message, data = results.data});
            }
            catch (Exception e)
            {
                return Json(new { status = 500, mess = e.Message, data = e });
            }
        }
        [HttpPost("[action]")]
        public JsonResult UpdateStoreStatus(int status, int RVCNo)
        {
            try
            {
                _imsManageService.UpdateStoreStatus(status, RVCNo);
                return Json(new { status = 200, message = "Update Success"});
            }
            catch (Exception e)
            {
                return Json(new { status = 500, mess = e.Message, data = e });
            }
        }
        [HttpDelete("[action]")]
        public JsonResult DeleteStore(int RVCNo)
        {
            try
            {
                _imsManageService.DeleteStore(RVCNo);
                return Json(new { status = 200, message = "Delete Success" });
            }
            catch (Exception e)
            {
                return Json(new { status = 500, mess = e.Message, data = e });
            }
        }
        [HttpGet("[action]")]
        public JsonResult LoadStoreStatus()
        {
            try
            {
                var results = _imsManageService.LoadStoreStatus();

                return Json(new { status = 200, message = "Load Data Success", data = results });
            }
            catch (Exception e)
            {
                return Json(new { status = 500, mess = e.Message, data = e });
            }
        }
    }
}
