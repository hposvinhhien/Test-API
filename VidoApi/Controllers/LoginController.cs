using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Promotion.Application.Services;
using Promotion.Model.Model;

namespace VidoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILoginService _logService;
        public LoginController(IConfiguration config, ILoginService logService)
        {
            _config = config;
            _logService = logService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            IActionResult response = Unauthorized();
            string result = _logService.LoginUser(userLogin.Username, userLogin.Password);
            if (result != null)
            {
                //var tokenString = GenerateJSONWebToken(userLogin);
                response = Ok(new { token = "Bearer " + result });
            }
            else
            {
                response = BadRequest(new { Message = "Email or Password is incorrect" });
            }

            return response;
        }

        [HttpGet]
        [Route("getJWTClaim")]
        public IActionResult getJWTClaim(string token)
        {
            IActionResult response = Unauthorized();
            var result = _logService.getJWTTokenClaim(token);
            if (result != null)
            {
                //var tokenString = GenerateJSONWebToken(userLogin);
                response = Ok(new { token = result });
            }

            return response;
        }
    }

}
