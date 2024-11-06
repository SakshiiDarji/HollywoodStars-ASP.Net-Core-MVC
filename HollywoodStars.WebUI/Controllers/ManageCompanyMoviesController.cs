using HollywoodStars.Data;
using HollywoodStars.Models;
using HollywoodStars.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HollywoodStars.WebUI.Controllers
{
    [Authorize]
    public class ManageCompanyMoviesController : Controller
    {
        private readonly HollywoodStarsContext _context;

        public ManageCompanyMoviesController(HollywoodStarsContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id)
        {
            if (id == null) return RedirectToAction("Index", "NotFound", new { entity = "Company", backUrl = "/ManageCompanies/" });

            Company? company = null;
            List<Movie> listOfAssociatedMovies = new List<Movie>();
            List<Movie> listOfNonAssociatedMovies = new List<Movie>();

            try
            {
                company = await CompaniesData.GetCompany((int)id, _context);
                if (company == null) return RedirectToAction("Index", "NotFound", new { entity = "Company", backUrl = "/ManageCompanies/" });
                listOfAssociatedMovies = await CompanyMoviesData.GetAssociatedMovieList((int)id, _context);
                listOfNonAssociatedMovies = await CompanyMoviesData.GetNonAssociatedMovieList((int)id, _context);
            }
            catch (Exception ex)
            {
                TempData["DangerMessage"] = ex.Message;
            }

            var myViewModel = new CompanyMoviesViewModel();
            myViewModel.Company = company!;
            myViewModel.AssociatedMovies = listOfAssociatedMovies;
            myViewModel.NonAssociatedMovies = listOfNonAssociatedMovies;

            return View(myViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int companyId, int movieId)
        {
            try
            {
                await CompanyMoviesData.Insert(companyId, movieId, _context);
            }
            catch (Exception ex)
            {
                TempData["DangerMessage"] = ex.Message;
            }
            return RedirectToAction(nameof(Index), new { id = companyId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int companyId, int movieId)
        {
            try
            {
                await CompanyMoviesData.Delete(companyId, movieId, _context);
            }
            catch (Exception ex)
            {
                TempData["DangerMessage"] = ex.Message;
            }
            return RedirectToAction(nameof(Index), new { id = companyId });
        }
    }
}
