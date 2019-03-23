using DataLayer.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceLayer.MovieServices
{
    public class MovieDto
    {
        public int MovieId { get; set; }

        public string Title { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        public decimal Price { get; set; }

        public int GenreId { get; set; }

        [Display(Name = "Genre")]
        public string GenreName { get; set; }
    }
}
