using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.ViewModels
{
    public class Admin
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        public int Phone { get; set; }

        [Required]
        [MaxLength(60)]
        public string Email { get; set; }

        [Required]
        [MaxLength(20)]
        public string Password { get; set; }
        
        [MaxLength(4)]
        public string Category { get; set; }
    }
}
