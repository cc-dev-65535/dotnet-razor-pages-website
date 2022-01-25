using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Blogger.Pages.CategoryModel;

namespace Blogger
{
    public class ArticleService
    {
        private readonly AppDbContext _appDbContext;
        private int pageResults = 4;

        public ArticleService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateArticle(InputModel input, string pageCategory)
        {
            var article = new Article
            {
                Author = input.Author,
                Timestamp = DateTime.Now,
                Category = pageCategory,
                Title = input.Title,
                Text = input.Text,
            };
            _appDbContext.Add(article);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<ICollection<ArticleViewModel>> GetArticles(string pageCategory = "" , int pageNum = 1)
        {
            int skipCount = (pageNum - 1) * pageResults;
            if (string.IsNullOrEmpty(pageCategory))
            {
                return await _appDbContext.Articles
                                 //.Where(a => !a.IsDeleted)
                                 .OrderByDescending(a => a.Timestamp)
                                 .Skip(skipCount)
                                 .Take(pageResults)
                                 .Select(a => new ArticleViewModel
                                 {
                                     Author = a.Author,
                                     Category = a.Category.ToUpper(),
                                     Title = a.Title,
                                     Text = a.Text,
                                     //Timestamp = HelperFunctions.FormatDate(a.Timestamp)
                                     Timestamp = a.Timestamp.ToString("t") + " " + a.Timestamp.ToString("MMM,dd,yyyy")
                                 })
                                .ToListAsync();
            }

            return await _appDbContext.Articles
                                 //.Where(a => !a.IsDeleted)
                                 .Where(a => a.Category.Equals(pageCategory))
                                 .OrderByDescending(a => a.Timestamp)
                                 .Skip(skipCount)
                                 .Take(pageResults)
                                 .Select(a => new ArticleViewModel
                                 {
                                     Author = a.Author,
                                     Category = a.Category.ToUpper(),
                                     Title = a.Title,
                                     Text = a.Text,
                                     //Timestamp = HelperFunctions.FormatDate(a.Timestamp)
                                     Timestamp = a.Timestamp.ToString("t") + " " + a.Timestamp.ToString("MMM,dd,yyyy")
                                 })
                                .ToListAsync();
        }

        public async Task<int> GetArticlesCount(string pageCategory = "")
        {
            if (string.IsNullOrEmpty(pageCategory))
            {
                return await _appDbContext.Articles.CountAsync();
            }

            return await _appDbContext.Articles
                                     .Where(a => a.Category.Equals(pageCategory))
                                     .CountAsync();
        }
    }

    public class HelperFunctions
    {
        public static string FormatDate(DateTime date)
        {
            string[] monthNames = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            int year = date.Year;
            string month = monthNames[date.Month-1];
            int day = date.Day;
            int hour = date.Hour;
            int minute = date.Minute;
            return $"{hour}:{minute} {month}-{day}-{year}";
        }
    }
}
