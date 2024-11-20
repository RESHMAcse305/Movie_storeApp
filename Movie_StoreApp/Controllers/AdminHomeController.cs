using Microsoft.AspNetCore.Mvc;
using Movie_StoreApp.Models;

namespace Movie_StoreApp.Controllers
{
    public class AdminHomeController : Controller
    {
        AdminDB dbobj = new AdminDB();
        public IActionResult Admin_Home()
        {
            var movies = dbobj.SelectMovieList() ?? new List<MovieInsert>(); 
            ViewBag.Genres = GetGenres() ?? new List<Genre>(); ;
            ViewBag.Directors = GetDirectors() ?? new List<Director>(); ;
            if (!movies.Any())
            {
                TempData["msg"] = "No movies found.";
                return RedirectToAction("Admin_Home");
            }
            return View(movies);
        }
        [HttpGet]
        public IActionResult Admin_load()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Admin_pageclick(Genre objcls)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string resp = dbobj.InsertGenre(objcls);
                    TempData["msg"] = resp;
                    return RedirectToAction("Admin_load");
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View("Admin_load");
        }
        [HttpGet]
        public IActionResult Add_Director()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Admin_Director(Director objcls)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string resp = dbobj.InsertDirector(objcls);
                    TempData["msg"] = resp;
                    return RedirectToAction("Add_Director");
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View("Add_Director");

        }



        private List<Genre> GetGenres()
        {
            // Retrieve genres from database
            return dbobj.GetGenres();
        }

        private List<Director> GetDirectors()
        {
            // Retrieve directors from database
            return dbobj.GetDirectors();
        }



        [HttpGet]
        public IActionResult Add_Movie()
        {
            ViewBag.Genres = GetGenres();
            ViewBag.Directors = GetDirectors();
            return View();
        }
        [HttpPost]
        public IActionResult Admin_Movie(MovieInsert objcls)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (objcls.PosterFile != null)
                    {
                        var fileName = Path.GetFileName(objcls.PosterFile.FileName);
                        var filePath = Path.Combine("wwwroot/uploads", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            objcls.PosterFile.CopyTo(stream);
                        }

                        // Save the path in PosterImage property to store in the database
                        objcls.Poster_Image = "/uploads" + fileName;
                    }

                    string resp = dbobj.MovieInsert(objcls);
                    TempData["msg"] = resp;
                    return RedirectToAction("Add_Movie");
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            ViewBag.Genres = GetGenres();
            ViewBag.Directors = GetDirectors();
            return View("Add_Movie");

        }

        public IActionResult MovieDelete(int id)
        {
            try
            {
                string result = dbobj.DeleteMovie(id);
                TempData["msg"] = result;
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("Admin_Home");
        }

        public IActionResult MovieEdit(int id)
        {
            MovieInsert getList = dbobj.MovieSelect(id);
            ViewBag.Genres = GetGenres() ?? new List<Genre>();
            ViewBag.Directors = GetDirectors() ?? new List<Director>();
            if (getList == null)
            {
                TempData["msg1"] = "Movie not found";
                return RedirectToAction("Admin_Home");
            }
            return View(getList);
        }
        public IActionResult MUpdate(MovieInsert objcls)

        {
            try
            {
                if (ModelState.IsValid)
                {
                    string uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                    if (!Directory.Exists(uploadDir))
                    {
                        Directory.CreateDirectory(uploadDir);
                    }
                    if (objcls.PosterFile != null)
                    {
                        string fileName = Path.GetFileName(objcls.PosterFile.FileName);
                        string filePath = Path.Combine(uploadDir, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            objcls.PosterFile.CopyTo(stream);
                        }

                        objcls.Poster_Image = "/uploads/" + fileName;
                    }
                    string result = dbobj.UpdMovie(objcls);
                    TempData["msg1"] = result;
                    return RedirectToAction("MovieEdit", new { id = objcls.Movie_Id });
                }
                else
                {
                    
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    TempData["msg1"] = "Validation failed: " + string.Join(", ", errors);
                }
            }
            catch (Exception ex)
            {
                TempData["msg1"] = "Error: " + ex.Message;
            }
            ViewBag.Genres = GetGenres() ?? new List<Genre>();
            ViewBag.Directors = GetDirectors() ?? new List<Director>();
            return View("MovieEdit", objcls);
        }


    }



}

