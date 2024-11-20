using Microsoft.AspNetCore.Mvc;
using Movie_StoreApp.Models;

namespace Movie_StoreApp.Controllers
{
    public class LoginDBController : Controller
    {
        LoginDB dbobj = new LoginDB();

        [HttpGet]
        public IActionResult login_pageload()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login_click(Login objcls)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("login_pageload");
                }

                string login = dbobj.LoginDb(objcls, TempData);

                if (!string.IsNullOrEmpty(login))
                {
                    string uType = TempData["type"] as string;
                    var userId = TempData["uid"] as string;

                    if (login == "Admin" && uType == "Admin")
                    {
                        return RedirectToAction("Admin_Home", "AdminHome");
                    }
                    else if (login == "user" && uType == "user")
                    {
                        return RedirectToAction("User_Index", "User", new { id = objcls.userId });
                    }
                    else
                    {
                        TempData["Message"] = "Invalid username or password.";
                    }
                }
                else
                {
                    TempData["Message"] = "Invalid login credentials.";
                }
            }
            catch (Exception ex)    

            {
                TempData["Message"] = "An error occurred: " + ex.Message;
            }

            return RedirectToAction("login_pageload");
        }
    }
}
