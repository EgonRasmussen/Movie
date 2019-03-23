using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.MovieServices;
using System.Threading.Tasks;

namespace RazorPagesMovie.Pages.Movies
{
    public class EditModel : PageModel
    {
        private readonly IMovieService _movieService;
        private readonly IGenreService _genreService;

        public EditModel(IMovieService movieService, IGenreService genreService)
        {
            _movieService = movieService;
            _genreService = genreService;
        }

        [BindProperty]
        public Movie Movie { get; set; }

        public SelectList Genres { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie = await _movieService.GetMovieById(id.Value);

            Genres = new SelectList(await _genreService.GetGenres(), nameof(Genre.GenreId), nameof(Genre.GenreName), Movie.GenreId );

            if (Movie == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _movieService.UpdateMovie(Movie);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_movieService.MovieExists(Movie.MovieId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}