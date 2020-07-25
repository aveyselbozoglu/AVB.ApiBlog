using AVB.ApiBlog.Entities.DtoModels;
using AVB.ApiBlog.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AVB.ApiBlog.DataAccess.Interfaces
{
    public interface IArticleDal : IGenericRepository<Article>
    {
        Task<Article> GetArticleWithCategoryWithComment(int id);

        Task<List<Article>> GetArticlesWithIncludeWithLimit(int page, int pageSize);

        Task<List<Article>> GetArticlesWithCategoryWithComment(int page, int pageSize);

        Task<IEnumerable<Article>> GetArticlesByCategory(int categoryId);

        Task<IEnumerable<Article>> SearchArticles(string searchText);

        Task<IEnumerable<Article>> GetArticlesByMostView();

        Task<IEnumerable<Article>> GetArticlesArchieve();

        Task<IEnumerable<Article>> GetArticlesArchiveList(int year, int month);

        Task<IEnumerable<Article>> GetArticlesOrderedDescByPublishDate();

        Task<IEnumerable<ArticleReadDtoForDataTable>> GetArticlesForDataTable();

        new Task<int> Update(Article article);

        Task ArticleViewCountUp(int id);
    }
}