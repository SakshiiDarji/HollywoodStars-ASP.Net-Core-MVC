using HollywoodStars.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HollywoodStars.Data
{
    public static class MoviesData
    {

        public static async Task Insert(Movie movie, HollywoodStarsContext context)
        {
            if (movie.ReleaseYear < DateTime.Now.Year)
                throw new Exception("Release Year should be equal or more than current year!");

            movie.CreationDate = DateTime.Now;
            context.Movies.Add(movie);
            await context.SaveChangesAsync();
        }

        public static async Task Update(Movie movie, HollywoodStarsContext context)
        {
            if (movie.ReleaseYear < DateTime.Now.Year)
                throw new Exception("Release Year should be equal or more than current year!");

            var entity = context.Movies.Update(movie);
            entity.Property(c => c.CreationDate).IsModified = false;
            await context.SaveChangesAsync();
        }

        public static async Task<Movie?> GetMovie(int movieId, HollywoodStarsContext context)
        {
            return await context.Movies.FindAsync(movieId);
        }

        public static async Task<List<Movie>> GetList(HollywoodStarsContext context)
        {
            return await context.Movies.ToListAsync();
        }

        public static async Task Delete(Movie movie, HollywoodStarsContext context)
        {
            context.Movies.Remove(movie);
            await context.SaveChangesAsync();
        }
        public static async Task<bool> HasCompanies(Movie movie, HollywoodStarsContext context)
        {
            return await context.CompanyMovies.AnyAsync(ab => ab.MovieId == movie.MovieId);
        }
    }
}
