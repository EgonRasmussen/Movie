using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
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

        public IQueryable<MovieDto> GetMovies()
        {
            return _context.Movies.Include(m => m.Genre)
                .AsNoTracking()
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
            var movies = _context.Movies
                .AsNoTracking()
                .Select(m => new MovieDto
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

        public async Task<MovieDto> GetMovieById(int id)
        {
            return await _context.Movies.Include(m => m.Genre)
                .Where(m => m.MovieId == id)
                .Select(m => new MovieDto
                {
                    Id = m.MovieId,
                    Title = m.Title,
                    ReleaseDate = m.ReleaseDate,
                    Price = m.Price,
                    GenreId = m.GenreId,
                    GenreName = m.Genre.GenreName
                })
                .SingleOrDefaultAsync();
        }


        public async Task UpdateMovie(MovieDto movieDto)
        {
            Movie movie = new Movie
            {
                MovieId = movieDto.Id,
                Title = movieDto.Title,
                ReleaseDate = movieDto.ReleaseDate,
                Price = movieDto.Price,
                GenreId = movieDto.GenreId
            };

            _context.Attach(movie).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.MovieId == id);
        }

        public async Task DeleteMovie(int id)
        {
            _context.Movies.Remove(new Movie { MovieId = id });
            await _context.SaveChangesAsync();
        }

        public async Task CreateMovie(MovieDto movieDto)
        {
            Movie movie = new Movie
            {
                Title = movieDto.Title,
                ReleaseDate = movieDto.ReleaseDate,
                Price = movieDto.Price,
                GenreId = movieDto.GenreId
            };

            _context.Add(movie);
            await _context.SaveChangesAsync();
        }
    }
}
