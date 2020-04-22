using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odh.BooksDemo.Entities
{
    public class Author
    {
        public int AuthorId { get; set; }
        [Required(ErrorMessage = "Author Name is required")]
        public string AuthorName { get; set; }
        [Required(ErrorMessage = "ISBN is required")]
        public string IsbNumber { get; set; }
    }
}
