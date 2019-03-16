using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceLayer.MovieServices
{
    public class MovieService : IMovieService
    {
        private readonly RazorPagesMovieContext _context;

        public MovieService(RazorPagesMovieContext context)
        {
            _context = context;
        }

        public  IQueryable<MovieDto> GetMovies()
        {
            return _context.Movies.Include(m => m.Genre)
                .Select(m => new MovieDto
                {
                    Id = m.MovieId,
                    Title = m.Title,
                    ReleaseDate = m.ReleaseDate,
                    Price = m.Price,
                    GenreName = m.Genre.GenreName
                });
        }

        public IQueryable<MovieDto> GetMovies(string searchString, int genreId)
        {
            var movies = _context.Movies.Select(m => new MovieDto
            {
                Id = m.MovieId,
                Title = m.Title,
                ReleaseDate = m.ReleaseDate,
                Price = m.Price,
                GenreId = m.GenreId,
                GenreName = m.Genre.GenreName
            });

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            if (genreId > 0)
            {
                movies = movies.Where(x => x.GenreId == genreId);
            }

            return movies;
        }
    }
}
