using System.ComponentModel.DataAnnotations;

namespace NewsSite.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Display(Name = "نویسنده")]
        public string Authors { get; set; }

        [Display(Name = "نظر")]
        public string Text { get; set; }

        public bool IsApproved { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreatedAt { get; set; }

        public int NewsId { get; set; }
        public NewsItem News { get; set; }
    }
}
