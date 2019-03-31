using Microsoft.VisualStudio.TestTools.UnitTesting;
using RazorPagesMovie.Data;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class UnitTestDataLayer
    {
        [TestMethod]
        public void QueryOfMovies()
        {
            using (var db = new RazorPagesMovieContext(Utilities.TestDbContextOptions()))
            {
                // ARRANGE
                Utilities.Initialize(db);

                // ACT
                var movies = db.Movies.ToList();

                // ASSERT
                Assert.AreEqual(4, movies.Count());
                Assert.AreEqual("When Harry Met Sally", movies.First().Title);
                Assert.AreEqual("Romantic Comedy", movies.First().Genre.GenreName);
            }
        }

        [TestMethod]
        public void UpdateOfMovie()
        {
            using (var db = new RazorPagesMovieContext(Utilities.TestDbContextOptions()))
            {
                // ARRANGE
                Utilities.Initialize(db);

                // ACT
                var movie = db.Movies.First();
                movie.Title = "New Title";
                movie.GenreId = 3;
                db.SaveChanges();

                // ASSERT
                movie = db.Movies.First();
                Assert.AreEqual("New Title", movie.Title);
                Assert.AreEqual(3, movie.GenreId);
            }
        }
    }
}
