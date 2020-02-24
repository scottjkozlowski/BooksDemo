using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Odh.BooksDemo.Entities
{
    public class Book
    {
        private readonly string _genre;
        public int BookId { get; set; }
        [Required(ErrorMessage = "Book Name is required")]
        public string BookName { get; set; }
        [Required(ErrorMessage = "ISBN is required")]
        public string IsbNumber { get; set; }

        [Required(ErrorMessage = "Please select a Genre")]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        [NotMapped]
        public string Genre
        {
            get
            {
                return GenreId == 0 ? string.Empty : ((Genre)GenreId).GetDescription();
            }
        }

        [Display(Name = "Date Published")]
        public DateTime? PublishedDate { get; set; }

    }
}
