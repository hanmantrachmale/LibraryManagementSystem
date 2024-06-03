using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "{0} can have max of {1} characters")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(5, ErrorMessage = "{0} can have max of {1} characters")]
        public string Author { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\d{1,5}$", ErrorMessage = "Invalid ISBN format")]
        public string ISBN { get; set; } = string.Empty;
        public bool IsBorrowed { get; set; }

    }
}
