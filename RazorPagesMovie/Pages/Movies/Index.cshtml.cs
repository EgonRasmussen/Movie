using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using ServiceLayer.MovieServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesMovie.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly IMovieService _service;

        public IndexModel(IMovieService service)
        {
            _service = service;
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

            //var movies = from m in _context.Movie
            //             select m;

            //if (!String.IsNullOrEmpty(SearchString))
            //{
            //    movies = movies.Where(s => s.Title.Contains(SearchString));
            //}

            //if (!String.IsNullOrEmpty(MovieGenre))
            //{
            //    //movies = movies.Where(x => x.GenreId == MovieGenre);
            //}
            //Genres = new SelectList(await genreQuery.Distinct().ToListAsync());

            Movies = await _service.GetMovies().ToListAsync();
        }
    }
}