using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // اضافه‌شده
using Microsoft.AspNetCore.Http;                  // اضافه‌شده

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

        [Display(Name = "مسیر تصویر")]
        public string? ImagePath { get; set; }    // فیلد جدید

        public List<Comment> Comments { get; set; }

        [NotMapped]
        [Display(Name = "انتخاب تصویر")]
        public IFormFile? ImageFile { get; set; }  // فیلد آپلود
    }
}