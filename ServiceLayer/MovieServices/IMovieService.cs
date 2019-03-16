using System.Linq;
using System.Threading.Tasks;

namespace ServiceLayer.MovieServices
{
    public interface IMovieService
    {
        IQueryable<MovieDto> GetMovies();
        IQueryable<MovieDto> GetMovies(string searchString, int genreId);
        Task <MovieDto> GetMovieById(int id);
        bool MovieExists(int id);
        Task UpdateMovie(MovieDto movie);
        Task DeleteMovie(int id);
        Task CreateMovie(MovieDto movie);
    }
}
