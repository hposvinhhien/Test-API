using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Promotion.Application.Services;

namespace VidoApi.Controllers
{
    [Route("api/[controller]")]
    public class PublicController : Controller
    {
        private readonly ILoginService _loginService;
        public PublicController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        [HttpGet]
        [HttpGet("[action]")]
        public JsonResult Receipt()
        {
            return Json(new { status = 200 });
        }
    }
}

