using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Blogger.Pages
{
    public class CategoryModel : PageModel
    {
        private readonly ILogger<CategoryModel> _logger;

        [BindProperty]
        public InputModel Input { get; set; }

        public List<string> Items { get; set; }

        public CategoryModel(ILogger<CategoryModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(int pageno, string category)
        {

        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage("./Index");
        }

        public class InputModel
        {
            public string Author { get; set; }
            public string Text { get; set; }
        }
    }
}
