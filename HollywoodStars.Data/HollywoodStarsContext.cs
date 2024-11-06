using HollywoodStars.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HollywoodStars.Data
{
    public class HollywoodStarsContext : IdentityDbContext
    {
        public HollywoodStarsContext(DbContextOptions options) : base(options) { }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<CompanyMovie> CompanyMovies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Company>()
                .HasMany(aCompany => aCompany.Movies) // A comapny can have many movies 
                .WithMany(aMovie => aMovie.Companies) //A Movie can have many companies
                .UsingEntity<CompanyMovie>();
        }
    }
}
