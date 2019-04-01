using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using System.Linq;
using UnitTests.Utilities;
using DataLayer.Models;

namespace UnitTests
{
    [TestClass]
    public class UnitTestDataLayer
    {
        [TestMethod]
        public void GetMovies_Count_Title_GenreName()
        {
            using (var db = new RazorPagesMovieContext(SqlContext.TestDbContextOptions()))
            {
                // ARRANGE
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                // ACT
                var movies = db.Movies.Include(g => g.Genre).ToList();

                // ASSERT
                Assert.AreEqual(4, movies.Count());
                Assert.AreEqual("When Harry Met Sally", movies.First().Title);
                Assert.AreEqual("Romantic Comedy", movies.First().Genre.GenreName);
            }
        }

        [TestMethod]
        public void UpdateMovie_MovieTitle_Genre()
        {
            using (var db = new RazorPagesMovieContext(SqlContext.TestDbContextOptions()))
            {
                // ARRANGE

                // ACT
                Movie movie = db.Movies.First();
                movie.Title = "New Title";
                movie.GenreId = 3;
                db.SaveChanges();

                // ASSERT
                movie = db.Movies.Include(g => g.Genre).First();
                Assert.AreEqual("New Title", movie.Title);
                Assert.AreEqual("Western", movie.Genre.GenreName);
            }
        }
    }
}
