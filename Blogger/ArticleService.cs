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

        public async void CreateArticle(InputModel Input)
        {
            var article = new Article
            {
                Author = Input.Author,
                Title = Input.Title,
                Text = Input.Text,
            };
            _appDbContext.Add(article);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<ICollection<RecipeSummaryViewModel>> GetRecipes()
        {

        }
    }
}
