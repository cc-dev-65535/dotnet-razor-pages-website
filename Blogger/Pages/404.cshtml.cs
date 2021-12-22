using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Blogger.Pages
{
    public class _404Model : PageModel
    {
        private readonly ILogger<_404Model> _logger;

        public _404Model(ILogger<_404Model> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
        }
    }
}
