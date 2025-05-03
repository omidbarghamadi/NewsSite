using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewsSite.Models;
using System.Security.Principal;

namespace NewsSite.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<NewsItem> NewsItem { get; set; }
        public DbSet<Comment> Comment { get; set; }

    }

}
