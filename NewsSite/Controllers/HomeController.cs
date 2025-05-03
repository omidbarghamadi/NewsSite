using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsSite.Data;
using NewsSite.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var news = await _context.NewsItem
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
            return View(news);
        }

        public async Task<IActionResult> Details(int id)
        {
            var news = await _context.NewsItem
                .Include(n => n.Comments.Where(c => c.IsApproved))
                .FirstOrDefaultAsync(n => n.Id == id);

            if (news == null)
                return NotFound();

            return View(news);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(Comment comment)
        {
            comment.CreatedAt = DateTime.Now;
            comment.IsApproved = false;

            _context.Comment.Add(comment);
            await _context.SaveChangesAsync();

            TempData["CommentSuccessMessage"] = "نظر شما ثبت شد و پس از تأیید نمایش داده خواهد شد.";
            return RedirectToAction("Details", new { id = comment.NewsId });
        }
    }
}
