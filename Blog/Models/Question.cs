using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Question
    {

        public Question()
        {
            this.Date = DateTime.Now;
            this.Answers = new HashSet<Answer>();

        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public ApplicationUser Author { get; set; }

        
        public string Category { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
        


    }
}
