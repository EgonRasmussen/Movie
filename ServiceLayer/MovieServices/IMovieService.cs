using System.Linq;

namespace ServiceLayer.MovieServices
{
    public interface IMovieService
    {
        IQueryable<MovieDto> GetMovies();
    }
}
