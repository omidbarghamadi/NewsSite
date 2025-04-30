using Microsoft.AspNetCore.Mvc;
using NewsSite.Data;
using NewsSite.Models;
using Microsoft.EntityFrameworkCore;

public class NewsController : Controller
{
    private readonly ApplicationDbContext _context;

    public NewsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Details(int id)
    {
        var news = _context.NewsItem
            .Include(n => n.Comments)
            .FirstOrDefault(n => n.Id == id);

        if (news == null) return NotFound();
        return View(news);
    }

    [HttpPost]
    public IActionResult AddComment(int newsId, string authors, string text)
    {
        var comment = new Comment
        {
            NewsId = newsId,
            Authors = authors,
            Text = text,
            IsApproved = false
        };
        _context.Comment.Add(comment);
        _context.SaveChanges();

        TempData["Success"] = "نظر شما با موفقیت ثبت شد و پس از تأیید نمایش داده خواهد شد.";
        return RedirectToAction("Details", new { id = newsId });
    }
}
