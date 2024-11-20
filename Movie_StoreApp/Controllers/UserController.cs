using Microsoft.AspNetCore.Mvc;
using Movie_StoreApp.Models;

namespace Movie_StoreApp.Controllers
{
    public class UserController : Controller
    {
        UserDB dbobj = new UserDB();
        public IActionResult User_Index()
        {
            List<MovieInsert> getlist = dbobj.MovieList();
            return View(getlist);
        }
        public IActionResult User_click(int id)
        {
            Movie glist = dbobj.SelectMovie(id);
            return View(glist);
        }

        public IActionResult Review_load(int movieId)
        {
            if (TempData["uid"] != null)
            {
                int userId = Convert.ToInt32(TempData["uid"]);
                TempData.Keep("uid");

                var review = new Review
                {
                    User_Id = userId,
                    Movie_Id = movieId
                };

                return View("Review_load",review);
            }
            TempData["msg1"] = "User not logged in. Please log in to add a review.";
            return RedirectToAction("login_pageload", "LoginDB");
        }
        public IActionResult Review_click(Review clsobj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string resp = dbobj.AddReviews(clsobj);
                    TempData["msg1"] = resp;
                    return RedirectToAction("User_Index", "User");

                }
            }
            catch (Exception ex)
            {
                TempData["msg1"] = ex.Message;
            }
            return View("Review_load", clsobj);

        }
        public IActionResult Search_index(string searchTerm)
        {
            var movies = dbobj.SearchMovies(searchTerm);

            var viewModel = new MovieSearchViewModel
            {
                SearchTerm = searchTerm,
                Movies = movies
            };

            return View(viewModel);
        }
    }
}
