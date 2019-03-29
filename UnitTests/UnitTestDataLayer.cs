using Microsoft.VisualStudio.TestTools.UnitTesting;
using RazorPagesMovie.Data;
using System;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (var db = new RazorPagesMovieContext(Utilities.TestDbContextOptions()))
            {
                // Use the db here in the unit test.
            }
        }
    }
}
