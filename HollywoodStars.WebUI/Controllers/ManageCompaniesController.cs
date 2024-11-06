using HollywoodStars.Data;
using HollywoodStars.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HollywoodStars.WebUI.Controllers;

[Authorize]
public class ManageCompaniesController : Controller
    {
        private readonly HollywoodStarsContext _context;

        public ManageCompaniesController(HollywoodStarsContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var listOfCompanies = new List<Company>();
            try
            {
                listOfCompanies = await CompaniesData.GetList(_context);
            }
            catch (Exception ex)
            {
                TempData["DangerMessage"] = ex.Message;
            }
            return View(listOfCompanies);
        }

        public async Task<IActionResult> AddOrUpdate(int? id)
        {
            if (id == null) return View();

            Company? company = null;
            try
            {
                company = await CompaniesData.GetCompany((int)id, _context);
                if (company == null)
                    return RedirectToAction("Index", "NotFound", new { entity = "Company", backUrl = "/ManageCompanies/" });
            }
            catch (Exception ex)
            {
                TempData["DangerMessage"] = ex.Message;
            }
            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrUpdate(Company company)
        {
            if (!ModelState.IsValid)
                return (company.CompanyId == 0) ? View() : View(company);

            try
            {
                if (company.CompanyId == 0)
                    await CompaniesData.Insert(company, _context);
                else
                    await CompaniesData.Update(company, _context);
            }
            catch (Exception ex)
            {
                TempData["DangerMessage"] = ex.Message;
                return (company.CompanyId == 0) ? View() : View(company);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Remove(Company company)
        {
            try
            {
                if (await CompaniesData.HasMovies(company, _context))
                    throw new Exception("This Company cannot be removed because it has been associated with one or more movies. Remove all associations first.");
                else
                    await CompaniesData.Delete(company, _context);
            }
            catch (Exception ex)
            {
                TempData["DangerMessage"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }

