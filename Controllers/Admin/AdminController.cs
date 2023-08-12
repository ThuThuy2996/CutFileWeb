using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CutFileWeb.Controllers.Admin
{
    public class AdminController : Controller
    {
        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            return View(); 

            //var checkLogin = HttpContext.Session.GetString("UserName");
            //if (checkLogin != null)
            //{
            //    return View();
            //}
            //else
            //{
            //    return RedirectToAction("Login","Account");
            //}
        }
    }
}
