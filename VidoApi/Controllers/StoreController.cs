using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Promotion.Application.Services;
using Vido.Core.Service;

namespace PosAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StoreController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly IStoreService _storeService;
        public StoreController(IStoreService storeService, ILoginService loginService)
        {
            _storeService = storeService;
            _loginService = loginService;
        }

        [HttpGet]
        [Route("getInfoStore")]
        public JsonResult getInfoStore()
        {
            try
            {
                var accessToken = Request.Headers[HeaderNames.Authorization];
                var StoreId = _loginService.getStoreIdByToken(accessToken);
                var storeData = _storeService.GetStoreByID(StoreId);
                return Json(new { status = 200, message = "Success", data = storeData });
            }
            catch (Exception ex)
            {
                return Json(new { status = 500, message = ex.Message, data = ex });
            }
           
        }
    }
}
