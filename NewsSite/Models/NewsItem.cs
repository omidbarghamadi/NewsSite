using System.ComponentModel.DataAnnotations.Schema;

namespace NewsSite.Models
{
    public class NewsItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        //public string? ImagePath { get; set; }

        public List<Comment> Comments { get; set; }

        //[NotMapped]
        //public IFormFile? ImageFile { get; set; }
    }
}
