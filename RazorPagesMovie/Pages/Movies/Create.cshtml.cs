﻿using DataLayer.Models;
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

        [BindProperty]
        public Movie Movie { get; set; }
        public SelectList Genres { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            Genres = new SelectList(await _genreService.GetGenres(), nameof(Genre.GenreId), nameof(Genre.GenreName));

            Movie = new Movie
            {
                ReleaseDate = DateTime.Now
            };
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (Movie.GenreId == 0)
            {
                ModelState.AddModelError("Movie.Genre.GenreName", "Select a Genre");
            }

            if (!ModelState.IsValid)
            {
                Genres = new SelectList(await _genreService.GetGenres(), nameof(Genre.GenreId), nameof(Genre.GenreName));
                return Page();
            }

            await _movieService.CreateMovie(Movie);

            return RedirectToPage("./Index"); 
        }
    }
}