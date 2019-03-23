using DataLayer.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceLayer.MovieServices
{
    public interface IMovieService
    {
        IQueryable<MovieDto> GetMovies();
        IQueryable<MovieDto> GetMovies(string searchString, int genreId);
        Task <Movie> GetMovieById(int id);
        bool MovieExists(int id);
        Task UpdateMovie(Movie movie);
        Task CreateMovie(Movie movie);
        Task DeleteMovie(Movie movie);
    }
}
