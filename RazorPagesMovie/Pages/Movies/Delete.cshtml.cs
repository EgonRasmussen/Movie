using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceLayer.MovieServices;
using System.Threading.Tasks;

namespace RazorPagesMovie.Pages.Movies
{
    public class DeleteModel : PageModel
    {
        private readonly IMovieService _movieService;

        public DeleteModel(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [BindProperty]
        public Movie Movie { get; set; }

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie = await _movieService.GetMovieById(id.Value);

            if (Movie != null)
            {
                await _movieService.DeleteMovie(Movie);
            }

            return RedirectToPage("./Index");
        }
    }
}