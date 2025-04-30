using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsSite.Data;
using NewsSite.Models;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.Controllers
{
    [Authorize]
    public class AdminNewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminNewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var newsList = await _context.NewsItem
                 .OrderBy(n => n.CreatedAt)
                 .ToListAsync();

            var comments = await _context.Comment.ToListAsync();

            var approvedCounts = comments
                .Where(c => c.IsApproved)
                .GroupBy(c => c.NewsId)
                .ToDictionary(g => g.Key, g => g.Count());

            var pendingCounts = comments
                .Where(c => !c.IsApproved)
                .GroupBy(c => c.NewsId)
                .ToDictionary(g => g.Key, g => g.Count());

            ViewBag.ApprovedCounts = approvedCounts;
            ViewBag.PendingCounts = pendingCounts;

            return View(newsList);
        }

        public IActionResult Create()
        {
            return View(new NewsItem());
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewsItem newsItem)
        {
            if (newsItem.CreatedAt == null)
                newsItem.CreatedAt = DateTime.Now;
            _context.NewsItem.Add(newsItem);
            await _context.SaveChangesAsync();
            TempData["Success"] = "خبر ثبت شد";
            return RedirectToAction("Index");
            

            TempData["Error"] = "ثبت نشد";
            return View(newsItem);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var news = await _context.NewsItem.FindAsync(id);
            if (news == null) return NotFound();
            return View(news);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(NewsItem news)
        {
            if (ModelState.IsValid)
            {
                _context.Update(news);
                await _context.SaveChangesAsync();
                TempData["Success"] = "خبر با موفقیت ویرایش شد";
                return RedirectToAction("Index");
            }
            return View(news);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var news = await _context.NewsItem.FindAsync(id);
            if (news == null) return NotFound();
            _context.NewsItem.Remove(news);
            await _context.SaveChangesAsync();
            TempData["Success"] = "خبر حذف شد";
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var news = await _context.NewsItem.FindAsync(id);
            if (news != null)
            {
                _context.NewsItem.Remove(news);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Comments(int newsId)
        {
            var comments = await _context.Comment
                .Where(c => c.NewsId == newsId)
                .OrderBy(c => c.CreatedAt)
                .ToListAsync();

            return View(comments);
        }

        public async Task<IActionResult> ApproveComment(int commentId)
        {
            var comment = await _context.Comment.FindAsync(commentId);
            if (comment != null)
            {
                comment.IsApproved = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Comments", new { newsId = comment.NewsId });
        }
        public async Task<IActionResult> RejectComment(int commentId)
        {
            var comment = await _context.Comment.FindAsync(commentId);
            if (comment != null)
            {
                comment.IsApproved = false;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Comments", new { newsId = comment.NewsId });
        }
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var comment = await _context.Comment.FindAsync(commentId);
            if (comment != null)
            {
                _context.Comment.Remove(comment);
                _context.SaveChanges();
                TempData["Success"] = "نظر با موفقیت حذف شد.";
            }
            else
            {
                TempData["Error"] = "نظر مورد نظر یافت نشد.";
            }
            return RedirectToAction("Comments", new { newsId = comment.NewsId });
        }
    }
}
