using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogger.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ArticleService _articleService;

        public ICollection<ArticleViewModel> Articles { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ArticleService articleService)
        {
            _logger = logger;
            _articleService = articleService;
        }

        public async Task OnGetAsync()
        {
            Articles = await _articleService.GetArticles();
        }
    }
}