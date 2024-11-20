using Microsoft.AspNetCore.Mvc;

namespace Movie_StoreApp.Controllers
{
    public class SearchDBController : Controller
    {
        public IActionResult Search_pageload()
        {
            return View();
        }
        public IActionResult Search_click()
        {
            return View();
        }
    }
}
