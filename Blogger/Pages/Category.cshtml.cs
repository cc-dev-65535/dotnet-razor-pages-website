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
        public string PageCategory { get; set; }

        public CategoryModel(ILogger<CategoryModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(string category)
        {
            PageCategory = category;

            Articles = new List<InputModel>();
            InputModel article = new InputModel { Author = "a", Title = "b", Text = "b" };
            Articles.Add(article);
        }

        public IActionResult OnPost(string category)
        {
            PageCategory = category;

            Articles = new List<InputModel>();
            InputModel article = new InputModel { Author = "a", Title = "b", Text = "b" };
            Articles.Add(article);

            if (!ModelState.IsValid)
            {
                return Page();
            }

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
