using HollywoodStars.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HollywoodStars.Data
{
    public static class CompaniesData
    {

        public static async Task Insert(Company company, HollywoodStarsContext context)
        {
            company.CreationDate = DateTime.Now;
            context.Companies.Add(company);
            await context.SaveChangesAsync();
        }

        public static async Task Update(Company company, HollywoodStarsContext context)
        {

            var entity = context.Companies.Update(company);
            entity.Property(c => c.CreationDate).IsModified = false;
            await context.SaveChangesAsync();
        }

        public static async Task<Company?> GetCompany(int companyId, HollywoodStarsContext context)
        {
            return await context.Companies.FindAsync(companyId);
        }

        
        public static async Task<List<Company>> GetList(HollywoodStarsContext context)
        {
            return await context.Companies.ToListAsync();
        }

        public static async Task Delete(Company company, HollywoodStarsContext context)
        {
            context.Companies.Remove(company);
            await context.SaveChangesAsync();
        }

        public static async Task<bool> HasMovies(Company company, HollywoodStarsContext context)
        {
            return await context.CompanyMovies.AnyAsync(ab => ab.CompanyId == company.CompanyId);
        }
    }
}
