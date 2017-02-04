using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class LatinPage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public ApplicationUser Author { get; set; }
    }
}