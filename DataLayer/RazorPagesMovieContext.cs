using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace RazorPagesMovie.Data
{
    public class RazorPagesMovieContext : DbContext
    {
        public RazorPagesMovieContext(DbContextOptions<RazorPagesMovieContext> options)
            : base(options)
        {
        }


        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().HasData(
                 new Genre { GenreId = 1, GenreName = "Romantic Comedy" },
                 new Genre { GenreId = 2, GenreName = "Comedy" },
                 new Genre { GenreId = 3, GenreName = "Western" }
            );
            modelBuilder.Entity<Movie>().HasData(
                new Movie
                {
                    MovieId = 1,
                    Title = "When Harry Met Sally",
                    ReleaseDate = DateTime.Parse("1989-2-12"),
                    GenreId = 1,
                    Price = 7.99M
                },

                new Movie
                {
                    MovieId = 2,
                    Title = "Ghostbusters ",
                    ReleaseDate = DateTime.Parse("1984-3-13"),
                    GenreId = 2,
                    Price = 8.99M
                },

                new Movie
                {
                    MovieId = 3,
                    Title = "Ghostbusters 2",
                    ReleaseDate = DateTime.Parse("1986-2-23"),
                    GenreId = 2,
                    Price = 9.99M
                },

                new Movie
                {
                    MovieId = 4,
                    Title = "Rio Bravo",
                    ReleaseDate = DateTime.Parse("1959-4-15"),
                    GenreId = 3,
                    Price = 3.99M
                }
            );
        }
    }
}
