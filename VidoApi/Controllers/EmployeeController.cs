using Microsoft.AspNetCore.Mvc;

namespace VidoApi.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
