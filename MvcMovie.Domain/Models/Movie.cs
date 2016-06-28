using System;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Domain.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Movie title must be between 3 and 60 characters in length")]
        public string Title { get; set; }
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-z''-'\s]*$")]
        [Required]
        [StringLength(30)]
        public string Genre { get; set; }
        [Range(1, 100)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-z13\-\s]*$", ErrorMessage = "Field must include alphabetical characters, with an optional '-13' where necessary")]
        [StringLength(5, ErrorMessage = "The field cannot be greater than 5 characters in length")]
        public string Rating { get; set; }
    }
}