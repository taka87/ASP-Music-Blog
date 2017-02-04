using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Answer
    {
        public Answer()
        {
            this.Date = DateTime.Now;
        }
        public int Id { get; set; }
        public int QuestionId { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        [Required]
        public string Category { get; set; }

        public Question Question { get; set; }

    }
}