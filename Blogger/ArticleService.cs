using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Blogger.Pages.CategoryModel;

namespace Blogger
{
    public class ArticleService
    {
        private readonly AppDbContext _appDbContext;

        public ArticleService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateArticle(InputModel input, string pageCategory)
        {
            var article = new Article
            {
                Author = input.Author,
                Category = pageCategory,
                Title = input.Title,
                Text = input.Text,
            };
            _appDbContext.Add(article);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<ICollection<ArticleViewModel>> GetArticles(string pageCategory = "")
        {
            if (string.IsNullOrEmpty(pageCategory))
            {
                return await _appDbContext.Articles
                                 //.Where(a => !a.IsDeleted)
                                 .Select(a => new ArticleViewModel
                                 {
                                     Author = a.Author,
                                     Category = a.Category,
                                     Title = a.Title,
                                     Text = a.Text
                                 })
                                .ToListAsync();
            }

            return await _appDbContext.Articles
                                 //.Where(a => !a.IsDeleted)
                                 .Where(a => a.Category.Equals(pageCategory))
                                 .Select(a => new ArticleViewModel
                                 {
                                     Author = a.Author,
                                     Category = a.Category,
                                     Title = a.Title,
                                     Text = a.Text
                                 })
                                .ToListAsync();
        }
    }
}
