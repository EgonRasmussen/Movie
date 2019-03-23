using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        private readonly IMapper _mapper;

        public MovieService(RazorPagesMovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IQueryable<MovieDto> GetMovies()
        {
            return _context.Movies
                .AsNoTracking()
                .ProjectTo<MovieDto>();

            //return _context.Movies.Include(m => m.Genre)
            //   .AsNoTracking()
            //   .Select(m => new MovieDto
            //   {
            //       Id = m.MovieId,
            //       Title = m.Title,
            //       ReleaseDate = m.ReleaseDate,
            //       Price = m.Price,
            //       GenreName = m.Genre.GenreName
            //   });
        }

        public IQueryable<MovieDto> GetMovies(string searchString, int genreId)
        {
            var movies = _context.Movies
                .AsNoTracking();

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            if (genreId > 0)
            {
                movies = movies.Where(x => x.GenreId == genreId);
            }
            return movies.ProjectTo<MovieDto>();

            //    .Select(m => new MovieDto
            //    {
            //        Id = m.MovieId,
            //        Title = m.Title,
            //        ReleaseDate = m.ReleaseDate,
            //        Price = m.Price,
            //        GenreId = m.GenreId,
            //        GenreName = m.Genre.GenreName
            //    });
        }

        public async Task<Movie> GetMovieById(int id)
        {
            return await _context.Movies.Include(m => m.Genre)
                .Where(m => m.MovieId == id)
                .SingleOrDefaultAsync();
        }


        public async Task UpdateMovie(Movie movie)
        {
            _context.Attach(movie).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.MovieId == id);
        }

        public async Task CreateMovie(Movie movie)
        {
            _context.Add(movie);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMovie(Movie movie)
        {
            _context.Attach(movie).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
