
using Microsoft.AspNetCore.Mvc;
using Movie_StoreApp.Models;

namespace Movie_StoreApp.Controllers
{
    public class AdminDBController : Controller
    {
        AdminDB dbobj=new AdminDB();
        public IActionResult Admin_Pageload()
        {
            return View();
        }
        public IActionResult Admin_click(AdminInsert clsobj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string resp = dbobj.AdminDBInsert(clsobj);
                    TempData["msg"] = resp;
                    return RedirectToAction("Login", "LoginDB");

                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View("Admin_Pageload", clsobj);
          
        }
        public IActionResult Admin_home()
        {
            return View();
        }
    }
}
