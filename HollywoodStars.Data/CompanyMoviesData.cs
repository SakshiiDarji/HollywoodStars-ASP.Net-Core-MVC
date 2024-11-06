using HollywoodStars.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HollywoodStars.Data
{
    public static class CompanyMoviesData
    {

        public static async Task<List<Movie>> GetAssociatedMovieList(int companyId, HollywoodStarsContext context)
        {
          
            var result = await (from movie in context.Movies
                                where (from companyMovie in context.CompanyMovies
                                       where companyMovie.CompanyId == companyId
                                       select companyMovie.MovieId).Distinct()
                                       .Contains(movie.MovieId)
                                select movie).ToListAsync();
            return result;
        }

        public static async Task<List<Movie>> GetNonAssociatedMovieList(int companyId, HollywoodStarsContext context)
        {
           
            var result = await (from movie in context.Movies
                                where !(from companyMovie in context.CompanyMovies
                                        where companyMovie.CompanyId == companyId
                                        select companyMovie.MovieId).Distinct()
                                        .Contains(movie.MovieId)
                                select movie).ToListAsync();
            return result;
        }

        public static async Task<List<Company>> GetNonAssociatedCompanyList(int movieId, HollywoodStarsContext context)
        {
            var result = await (from company in context.Companies
                                where !(from companyMovie in context.CompanyMovies
                                        where companyMovie.MovieId == movieId
                                        select companyMovie.CompanyId).Distinct()
                                        .Contains(company.CompanyId)
                                select company).ToListAsync();
            return result;
        }

        public static async Task<List<Company>> GetAssociatedCompanyList(int movieId, HollywoodStarsContext context)
        {
            var result = await (from company in context.Companies
                                where (from companyMovie in context.CompanyMovies
                                        where companyMovie.MovieId == movieId
                                        select companyMovie.CompanyId).Distinct()
                                        .Contains(company.CompanyId)
                                select company).ToListAsync();
            return result;
        }

        public static async Task Insert(int companyId, int movieId, HollywoodStarsContext context)
        {
            var companyMovie = new CompanyMovie { CompanyId = companyId, MovieId = movieId, CreationDate = DateTime.Now };
            await context.CompanyMovies.AddAsync(companyMovie);
            await context.SaveChangesAsync();
        }

        public static async Task Delete(int companyId, int movieId, HollywoodStarsContext context)
        {
            var companyMovie = new CompanyMovie { CompanyId = companyId, MovieId = movieId };
            context.CompanyMovies.Remove(companyMovie);
            await context.SaveChangesAsync();
        }
    }
}
