using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Blogger.Pages
{
    public class CategoryModel : PageModel
    {
        private readonly ILogger<CategoryModel> _logger;
        private readonly ArticleService _articleService;

        [BindProperty]
        public InputModel Input { get; set; }

        public ICollection<ArticleViewModel> Articles { get; set; }
        public string PageCategory { get; set; }

        public CategoryModel(ILogger<CategoryModel> logger, ArticleService articleService)
        {
            _logger = logger;
            _articleService = articleService;
        }

        public async Task OnGetAsync(string category)
        {
            PageCategory = category;

            Articles = await _articleService.GetArticles(PageCategory);
        }

        public async Task<IActionResult> OnPostAsync(string category)
        {
            PageCategory = category;


            if (!ModelState.IsValid)
            {
                Articles = await _articleService.GetArticles(PageCategory);

                return Page();
            }

            await _articleService.CreateArticle(Input, PageCategory);
            Articles = await _articleService.GetArticles(PageCategory);
            return RedirectToPage("Category", new { category = category });
        }

        public class InputModel
        {
            [Required]
            [StringLength(40, ErrorMessage = "Maximum length is {1}")]
            [Display(Name = "Author")]
            public string Author { get; set; }

            [Required]
            [StringLength(40, ErrorMessage = "Maximum length is {1}")]
            [Display(Name = "Title")]
            public string Title { get; set; }

            [Required]
            [StringLength(500, ErrorMessage = "Maximum length is {1}")]
            [Display(Name = "Text")]
            public string Text { get; set; }
        }
    }
}
