using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWebsite.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Gde title?")]
        [MaxLength(100)]
        [Display(Name = "Post title")]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime Date { get; set; }

        [Display(Name = "Post main image")]
        [DataType(DataType.Upload)]
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public IEnumerable<PostTag> PostTags { get; set; }
    }
}
