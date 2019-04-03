using Microsoft.AspNetCore.Mvc;
using ServiceLayer.MovieServices;

namespace RazorPagesMovieWebApp.ViewComponents
{
    public class MovieCountViewComponent : ViewComponent
    {
        private readonly IMovieService _movieService;

        public MovieCountViewComponent(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public IViewComponentResult Invoke()
        {
            var count = _movieService.GetCountOfMovies();
            return View(count);
        }
    }

}
