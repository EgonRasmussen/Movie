using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPagesMovie.Data;
using ServiceLayer.MovieServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RazorPagesMovie.Pages.Movies
{
    public class CreateModel : PageModel
    {
        private readonly IMovieService _movieService;
        private readonly IGenreService _genreService;

        public CreateModel(IMovieService movieService, IGenreService genreService)
        {
            _movieService = movieService;
            _genreService = genreService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Genres = new SelectList(await _genreService.GetGenres(), nameof(Genre.GenreId), nameof(Genre.GenreName));

            Movie = new MovieDto
            {
                Title = "The Good, the bad, and the ugly",
                GenreId = 3,
                Price = 1.19M,
                ReleaseDate = DateTime.Now
            };
            return Page();
        }

        [BindProperty]
        public MovieDto Movie { get; set; }

        public SelectList Genres { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _movieService.CreateMovie(Movie);

            return RedirectToPage("./Index");
        }
    }
}