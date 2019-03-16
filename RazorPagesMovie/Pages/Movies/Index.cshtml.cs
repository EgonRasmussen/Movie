using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceLayer.MovieServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesMovie.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly IMovieService _movieService;
        private readonly IGenreService _genreService;

        public IndexModel(IMovieService movieService, IGenreService genreService)
        {
            _movieService = movieService;
            _genreService = genreService;
        }

        public IList<MovieDto> Movies { get; set; }
        
        // Requires using Microsoft.AspNetCore.Mvc.Rendering;
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public SelectList Genres { get; set; }

        [BindProperty(SupportsGet = true)]
        public string MovieGenre { get; set; }


        public async Task OnGetAsync()
        {
            Genres = new SelectList(await _genreService.GetGenres(), nameof(Genre.GenreId), nameof(Genre.GenreName));

            //Movies = _movieService.GetMovies().ToList();
            Movies = _movieService.GetMovies(SearchString, Convert.ToInt32(MovieGenre)).ToList();
        }
    }
}