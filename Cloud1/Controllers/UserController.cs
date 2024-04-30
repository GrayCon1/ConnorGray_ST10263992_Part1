using st10263992.Models;
using Microsoft.AspNetCore.Mvc;

namespace st10263992.Controllers
{
    public class UserController : Controller
    {
        public tblUser usrtbl=new tblUser();
        [HttpPost]
        public ActionResult About(tblUser Users)
        {
            var result = usrtbl.insert_User(Users);
            return RedirectToAction("Home", "About");
        }
        [HttpGet]
        public ActionResult About()
        {
            return View(usrtbl);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
