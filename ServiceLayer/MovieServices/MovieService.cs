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

        public async Task<MovieDto> GetMovieById(int id)
        {
            return await _context.Movies
                .Where(m => m.MovieId == id)
                .ProjectTo<MovieDto>()
                .SingleOrDefaultAsync();

            //    .Select(m => new MovieDto
            //    {
            //        MovieId = m.MovieId,
            //        Title = m.Title,
            //        ReleaseDate = m.ReleaseDate,
            //        Price = m.Price,
            //        GenreId = m.GenreId,
            //        GenreName = m.Genre.GenreName
            //    })
            //    .SingleOrDefaultAsync();
        }


        public async Task UpdateMovie(MovieDto movieDto)
        {
            //Movie movie = new Movie
            //{
            //    MovieId = movieDto.MovieId,
            //    Title = movieDto.Title,
            //    ReleaseDate = movieDto.ReleaseDate,
            //    Price = movieDto.Price,
            //    GenreId = movieDto.GenreId
            //};

            var movie = _mapper.Map<Movie>(movieDto);

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
            //Movie movie = new Movie
            //{
            //    Title = movieDto.Title,
            //    ReleaseDate = movieDto.ReleaseDate,
            //    Price = movieDto.Price,
            //    GenreId = movieDto.GenreId
            //};

            var movie = _mapper.Map<Movie>(movieDto);

            _context.Add(movie);
            await _context.SaveChangesAsync();
        }
    }
}
