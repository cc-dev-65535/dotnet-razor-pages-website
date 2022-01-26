using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blogger.Pages
{
    [EnsureValidCategoryAttribute]
    public class CategoryModel : PageModel
    {
        private readonly ILogger<CategoryModel> _logger;
        private readonly ArticleService _articleService;

        [BindProperty]
        public InputModel Input { get; set; }

        public ICollection<ArticleViewModel> Articles { get; set; }
        public string PageCategory { get; set; }
        public int PageNumber { get; set; }
        public int ArticleCount { get; set; }

        public CategoryModel(ILogger<CategoryModel> logger, ArticleService articleService)
        {
            _logger = logger;
            _articleService = articleService;
        }

        public async Task<IActionResult> OnGetAsync(string category, [FromQuery] int page)
        {
            if (!(page >= 0))
            {
                return NotFound();
            }
            if (page == 0)
            {
                page = 1;
            }
            PageCategory = category;
            PageNumber = page;
            ArticleCount = await _articleService.GetArticlesCount(PageCategory);
            Articles = await _articleService.GetArticles(PageCategory, page);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string category)
        {
            PageCategory = category;


            if (!ModelState.IsValid)
            {
                ArticleCount = await _articleService.GetArticlesCount(PageCategory);
                Articles = await _articleService.GetArticles(PageCategory);

                return Page();
            }

            await _articleService.CreateArticle(Input, PageCategory);
            return RedirectToPage("Category", new { category = category });
        }

        public class InputModel
        {
            [Required]
            [StringLength(50, ErrorMessage = "Maximum length is {1}")]
            [Display(Name = "Author")]
            public string Author { get; set; }

            [Required]
            [StringLength(50, ErrorMessage = "Maximum length is {1}")]
            [Display(Name = "Title")]
            public string Title { get; set; }

            [Required]
            [StringLength(500, ErrorMessage = "Maximum length is {1}")]
            [Display(Name = "Text")]
            public string Text { get; set; }
        }
    }

    public class EnsureValidCategoryAttribute: Attribute, IPageFilter
    {
        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
            
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            Regex regex = new Regex(@"^(gaming|sports|politics|news)$");
            var category = (string) context.HandlerArguments["category"];
            if (!regex.IsMatch(category))
            {
                context.Result = new NotFoundResult();
            }
        }

        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        { 
        
        }
    }
}
