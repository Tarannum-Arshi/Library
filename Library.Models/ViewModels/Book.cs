using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.ViewModels
{
    public class Book
    {
        [Key]
        public int Bookid { get; set; }
        [Required]
        [MaxLength(40)]
        public string BookName { get; set; }
        [Required]
        [MaxLength(40)]
        public string Author { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public int AvailableStock { get; set; }
    }
}
