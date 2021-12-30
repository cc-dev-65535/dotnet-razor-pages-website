using Microsoft.EntityFrameworkCore;
using System;

namespace Blogger
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Article> Articles { get; set; }
    }

    public class Article
    {
        public int ArticleID { get; set; }
        public string Category { get; set; }
        public string Author { get; set; }
        public DateTime Timestamp { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
