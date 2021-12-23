using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blogger.Pages
{
    public class CategoryModel : PageModel
    {
        private readonly ILogger<CategoryModel> _logger;

        [BindProperty]
        public InputModel Input { get; set; }

        public List<InputModel> Articles { get; set; }
        public string pageCategory { get; set; }

        public CategoryModel(ILogger<CategoryModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(string category)
        {
            pageCategory = category;

            Articles = new List<InputModel>();
            InputModel article = new InputModel { Author = "a", Text = "b" };
            Articles.Add(article);
        }

        public IActionResult OnPost(string category)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage("Category", new { category = category });
        }

        public class InputModel
        {
            [Required]
            [StringLength(100)]
            public string Author { get; set; }
            [Required]
            [StringLength(500)]
            public string Text { get; set; }
        }
    }
}
