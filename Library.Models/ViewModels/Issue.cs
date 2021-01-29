using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.ViewModels
{
    public class Issue
    {
        [Key]
        public int IssueId { get; set; }
        [ForeignKey("Admin")]
        public int UserId { get; set; }

        public Admin Admin { get; set; }

        [ForeignKey("Book")]
        public int Bookid { get; set; }

        public Book Book { get; set; }

       public DateTime IssueDate { get; set; }

         public DateTime ReturnDate { get; set; }

        [Required]
        [MaxLength(4)]
        public string Status { get; set; }
    }
}
