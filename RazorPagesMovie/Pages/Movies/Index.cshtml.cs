using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ServiceLayer.MovieServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RazorPagesMovie.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly IMovieService _movieService;
        private readonly IGenreService _genreService;
        private readonly IMemoryCache _cache;

        public IndexModel(IMovieService movieService, IGenreService genreService, IMemoryCache cache)
        {
            _movieService = movieService;
            _genreService = genreService;
            _cache = cache;
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
            //Genres = new SelectList(await _genreService.GetGenres(), nameof(Genre.GenreId), nameof(Genre.GenreName));

            #region GENRE CACHE (The long Road)
            // Look for cache key.
            if (!_cache.TryGetValue("GenresKey", out SelectList genreCacheEntry))
            {
                // Key not in cache, so get data.
                genreCacheEntry = new SelectList(await _genreService.GetGenres(), nameof(Genre.GenreId), nameof(Genre.GenreName));
            }

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time, reset time if accessed.
                    .SetSlidingExpiration(TimeSpan.FromSeconds(5));

            // Save data in cache with Option
            _cache.Set("GenresKey", genreCacheEntry, cacheEntryOptions);
            // Bind data to View
            Genres = genreCacheEntry;
            #endregion


            //Movies = await _movieService.GetMovies(SearchString, Convert.ToInt32(MovieGenre)).ToListAsync();

            #region MOVIES CACHE (The short Road)
            Movies = await _cache.GetOrCreate("MoviesKey", async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromSeconds(10);
                return await _movieService.GetMovies(SearchString, Convert.ToInt32(MovieGenre)).ToListAsync();
            });
           #endregion
        }
    }
}