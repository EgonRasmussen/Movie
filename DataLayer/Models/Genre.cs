using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class Genre
    {
        public int GenreId { get; set; }

        [Required]
        [Display(Name = "Genre Name")]
        public string GenreName { get; set; }


        public ICollection<Movie> Movies { get; set; }
    }
}
