﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace Library.Models.ViewModels
{//
    public class Cover
    {
        [Key]
        public int CoverId { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        [MaxLength(40)]
        public string BookName { get; set; }
        [Required]
        public int IssueId { get; set; }
    }
}
