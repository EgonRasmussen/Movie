using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.MovieServices;
using System.Threading.Tasks;

namespace RazorPagesMovie.Pages.Movies
{
    public class EditModel : PageModel
    {
        private readonly IMovieService _movieService;

        public EditModel(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [BindProperty]
        public MovieDto Movie { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie = await _movieService.GetMovieById(id.Value);

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