using System.ComponentModel.DataAnnotations;

namespace NewsSite.Models
{
    public class NewsItem
    {
        public int Id { get; set; }

        [Display(Name = "عنوان خبر")]
        public string Title { get; set; }

        [Display(Name = "خلاصه خبر")]
        public string Summary { get; set; }

        [Display(Name = "متن خبر")]
        public string Content { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public DateTime CreatedAt { get; set; }

        public List<Comment> Comments { get; set; }

        //[NotMapped]
        //public IFormFile? ImageFile { get; set; }
    }
}
