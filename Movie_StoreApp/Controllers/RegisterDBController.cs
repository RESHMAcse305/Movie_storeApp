using Microsoft.AspNetCore.Mvc;
using Movie_StoreApp.Models;

namespace Movie_StoreApp.Controllers
{
    public class RegisterDBController : Controller
    {
       RegisterDB dbobj = new RegisterDB();
        public IActionResult Register_pageload()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register_click(Registration obcls)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string resp = dbobj.InsertDB(obcls);
                    TempData["msg"] = resp;
                    return RedirectToAction("login_pageload", "LoginDB");

                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View("Register_pageload",obcls);
        }
    }
}
