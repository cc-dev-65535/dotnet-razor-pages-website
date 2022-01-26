using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogger.Pages
{
    public class ArchiveModel : PageModel
    {
        private readonly ILogger<ArchiveModel> _logger;
        private readonly ArticleService _articleService;

        public ICollection<ArticleViewModel> Articles { get; set; }
        public int PageNumber { get; set; }
        public int ArticleCount { get; set; }

        public ArchiveModel(ILogger<ArchiveModel> logger, ArticleService articleService)
        {
            _logger = logger;
            _articleService = articleService;
        }

        public async Task<IActionResult> OnGetAsync([FromQuery] int page)
        {
            if (!(page >= 0))
            {
                return NotFound();
            }
            if (page == 0)
            {
                page = 1;
            }
            PageNumber = page;
            ArticleCount = await _articleService.GetArticlesCount();
            Articles = await _articleService.GetArticles(pageNum: page);
            return Page();
        }
    }
}
