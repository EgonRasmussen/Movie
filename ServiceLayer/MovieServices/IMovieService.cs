using System.Linq;
using System.Threading.Tasks;

namespace ServiceLayer.MovieServices
{
    public interface IMovieService
    {
        IQueryable<MovieDto> GetMovies();
        IQueryable<MovieDto> GetMovies(string searchString, int genreId);
    }
}
