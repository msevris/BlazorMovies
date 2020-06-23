using BlazorMovies.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorMovies.Server
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MoviesActors>().HasKey(x => new { x.MovieId, x.PersonId });
            modelBuilder.Entity<MoviesGenres>().HasKey(x => new { x.MovieId, x.GenreId });
            // *** IT IS CORRECT 
            // but we use Sql Query instead 

            //var roleAdminId = "57f0e373-2d6f-4fff-90a2-c151d00c2410";
            //var userAdminId = "04aea431-6c56-4892-bc63-5929b77569ea";
            //var hasher = new PasswordHasher<IdentityUser>();
            //var userAdmin = new IdentityUser()
            //{
            //    Id = userAdminId,
            //    Email = "michael@hotmail.com",
            //    UserName = "michael@hotmail.com",
            //    NormalizedEmail = "michael@hotmail.com",
            //    NormalizedUserName = "michael@hotmail.com",
            //    EmailConfirmed = true,
            //    PasswordHash = hasher.HashPassword(null, "exPr0f!t")
            //};

            //modelBuilder.Entity<IdentityUser>().HasData(userAdmin);
            //modelBuilder.Entity<IdentityUserClaim<string>>().HasData(new IdentityUserClaim<string>()
            //{
            //    Id = 1,
            //    ClaimType = ClaimTypes.Role,
            //    ClaimValue = "Admin",
            //    UserId = userAdminId
            //});


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<MoviesActors> MoviesActors { get; set; }
        public DbSet<MoviesGenres> MoviesGenres { get; set; }
        public DbSet<MovieRating> MovieRatings { get; set; }
    }
}
