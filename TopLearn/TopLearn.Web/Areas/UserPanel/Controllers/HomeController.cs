using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TopLearn.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class HomeController : Controller
    {
        [Route("UserPanel")]
        public  IActionResult Index()
        {
            return View();
        }
    }
}
