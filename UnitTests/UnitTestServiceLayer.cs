using DataLayer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RazorPagesMovie.Data;
using ServiceLayer.MovieServices;
using System.Linq;
using System.Threading.Tasks;
using UnitTests.Utilities;

namespace UnitTests
{
    [TestClass]
    public class UnitTestServiceLayer
    {
        MovieService movieService;

        public UnitTestServiceLayer()
        {
            // ARRANGE - constructor is executed every time a TestMethod is called!
            var context = new RazorPagesMovieContext(InMemoryContext.TestDbContextOptions());
            context.Database.EnsureCreated();
            movieService = new MovieService(context);
        }

        [TestMethod]
        public async Task GetSingleMovieByService_Title_GenreName()
        {
            // ACT
            Movie movie = await movieService.GetMovieById(1);

            // ASSERT
            Assert.AreEqual("When Harry Met Sally", movie.Title);
            Assert.AreEqual("Romantic Comedy", movie.Genre.GenreName);
        }

        [TestMethod]
        public void GetAllMoviesByService_Count()
        {
            // ACT
            var movies = movieService.GetMovies();

            // ASSERT
            Assert.AreEqual(4, movies.Count());
        }

        [TestMethod]
        public async Task UpdateMovieByService_MovieTitle_Genre()
        {
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
