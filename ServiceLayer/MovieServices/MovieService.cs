using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using System.Linq;

namespace ServiceLayer.MovieServices
{
    public class MovieService : IMovieService
    {
        private readonly RazorPagesMovieContext _context;

        public MovieService(RazorPagesMovieContext context)
        {
            _context = context;
        }

        public IQueryable<MovieDto> GetMovies()
        {
            return _context.Movies.Include(m => m.Genre).Select(m => new MovieDto
            {
                Id = m.MovieId,
                Title = m.Title,
                ReleaseDate = m.ReleaseDate,
                Price = m.Price,
                Genre = m.Genre.GenreName
            });
        }
    }
}
