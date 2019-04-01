using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RazorPagesMovie.Data;

namespace UnitTests.Utilities
{
    public class SqlContext
    {
        public static DbContextOptions<RazorPagesMovieContext> TestDbContextOptions()
        {
            // Create a new service provider to create a new SQL database.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlServer()
                .BuildServiceProvider();

            // Create a new options instance using an SQL database and 
            // IServiceProvider that the context should resolve all of its services from.
            var builder = new DbContextOptionsBuilder<RazorPagesMovieContext>()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RazorPagesMovieContext-Layered;Trusted_Connection=True;MultipleActiveResultSets=true")
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
    }
}

