using HollywoodStars.Data;
using HollywoodStars.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace HollywoodStars.WebUI.Controllers
{
    [Authorize]
    public class ManageMoviesController : Controller
    {
        private readonly HollywoodStarsContext _context;

        public ManageMoviesController(HollywoodStarsContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var listOfMovies = new List<Movie>();
            try
            {
                listOfMovies = await MoviesData.GetList(_context);
            }
            catch (Exception ex)
            {
                TempData["DangerMessage"] = ex.Message;
            }

            return View(listOfMovies);
        }

        public async Task<IActionResult> AddOrUpdate(int? id)
        {
            Movie? movie = null;
            try
            {
                if (id == null) return View();
                movie = await MoviesData.GetMovie((int)id, _context);
                if (movie == null)
                    return RedirectToAction("Index", "NotFound", new { entity = "Movie", backUrl = "/ManageMovies/" });
            }
            catch (Exception ex)
            {
                TempData["DangerMessage"] = ex.Message;
            }
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrUpdate(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return (movie.MovieId == 0) ? View() : View(movie);
            }

            try
            {
                if (movie.MovieId == 0)
                    await MoviesData.Insert(movie, _context);
                else
                    await MoviesData.Update(movie, _context);
            }
            catch (Exception ex)
            {
                TempData["DangerMessage"] = ex.Message;
             
                return (movie.MovieId == 0) ? View() : View(movie);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(Movie movie)
        {
            try
            {
                if (await MoviesData.HasCompanies(movie, _context))
                    throw new Exception("This Movie cannot be removed because it has been associated with one or more companies. Remove all associations first.");
                else
                    await MoviesData.Delete(movie, _context);
            }
            catch (Exception ex)
            {
                TempData["DangerMessage"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
