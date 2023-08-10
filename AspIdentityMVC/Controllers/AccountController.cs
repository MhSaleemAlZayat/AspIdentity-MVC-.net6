using Microsoft.AspNetCore.Mvc;

namespace AspIdentityMVC.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
