using DataLayer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RazorPagesMovie.Data;
using ServiceLayer.MovieServices;
using System;
using System.Linq;
using System.Threading.Tasks;
using UnitTests.Utilities;

namespace UnitTests
{
    [TestClass]
    public class UnitTestServiceLayer
    {
        [TestMethod]
        public async Task GetSingleMovieByService_Title_GenreName()
        {
            // ARRANGE
            var context = new RazorPagesMovieContext(InMemoryContext.TestDbContextOptions());
            context.Database.EnsureCreated();
            MovieService movieService = new MovieService(context);

            // ACT
            Movie movie = await movieService.GetMovieById(1);

            // ASSERT
            Assert.AreEqual("When Harry Met Sally", movie.Title);
            Assert.AreEqual("Romantic Comedy", movie.Genre.GenreName);
        }

        [TestMethod]
        public void GetAllMoviesByService_Count()
        {
            // ARRANGE
            var context = new RazorPagesMovieContext(InMemoryContext.TestDbContextOptions());
            context.Database.EnsureCreated();
            MovieService movieService = new MovieService(context);

            // ACT
            var movies = movieService.GetMovies();

            // ASSERT
            Assert.AreEqual(4, movies.Count());
        }

        [TestMethod]
        public async Task UpdateMovieByService_MovieTitle_Genre()
        {
            // ARRANGE
            var context = new RazorPagesMovieContext(InMemoryContext.TestDbContextOptions());
            context.Database.EnsureCreated();
            MovieService movieService = new MovieService(context);

            // ACT
            Movie movie = await movieService.GetMovieById(1);
            movie.Title = "Update";
            movie.GenreId = 3;
            await movieService.UpdateMovie(movie);

            // ASSERT
            movie = await movieService.GetMovieById(1);
            Assert.AreEqual("Update", movie.Title);
            Assert.AreEqual("Western", movie.Genre.GenreName);
        }
    }
}
