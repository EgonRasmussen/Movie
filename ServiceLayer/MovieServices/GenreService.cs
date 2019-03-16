using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.MovieServices
{
    public interface IGenreService
    {
        Task<IEnumerable<Genre>> GetGenres();
    }

    public class GenreService : IGenreService
    {
        private readonly RazorPagesMovieContext _context;

        public GenreService(RazorPagesMovieContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genre>> GetGenres()
        {
            return await _context.Genres.ToListAsync();
        }
    }
}
