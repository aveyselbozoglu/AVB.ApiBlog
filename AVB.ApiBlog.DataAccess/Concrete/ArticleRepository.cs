using AVB.ApiBlog.DataAccess.EntityFrameworkCore;
using AVB.ApiBlog.DataAccess.Interfaces;
using AVB.ApiBlog.Entities.DtoModels;
using AVB.ApiBlog.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AVB.ApiBlog.DataAccess.Concrete
{
    public class ArticleRepository : GenericRepository<Article>, IArticleDal
    {
        public async Task<List<Article>> GetArticlesWithIncludeWithLimit(int page, int pageSize)
        {
            await using var context = new DatabaseContext();
            return await context.Articles.Include(x => x.Category).Include(x => x.Comments).OrderByDescending(x => x.PublishDate).Skip(pageSize * (page - 1)).Take(pageSize).ToListAsync();
        }

        public async Task<List<Article>> GetArticlesWithCategoryWithComment(int page, int pageSize)
        {
            await using var context = new DatabaseContext();
            return await context.Articles.Include(x => x.Category).Include(x => x.Comments).OrderByDescending(x => x.PublishDate).ToListAsync();
        }

        public async Task<IEnumerable<Article>> GetArticlesByCategory(int categoryId)
        {
            await using var context = new DatabaseContext();
            return await context.Articles.Include(x => x.Category).Include(x => x.Comments)
                .Where(x => x.CategoryId == categoryId).OrderByDescending(x => x.PublishDate).ToListAsync();
        }

        public async Task<IEnumerable<Article>> SearchArticles(string searchText)
        {
            await using var context = new DatabaseContext();

            return await context.Articles.Include(x => x.Category).Include(x => x.Comments).Where(x => x.Title.Contains(searchText)).OrderByDescending(x => x.PublishDate)
                .ToListAsync();
        }

        // en çok okunan makaleleri alır
        public async Task<IEnumerable<Article>> GetArticlesByMostView()
        {
            await using var context = new DatabaseContext();

            return await context.Articles
                .OrderByDescending(x => x.ViewCount).Take(5).ToListAsync();
        }

        public async Task<IEnumerable<Article>> GetArticlesArchieve()
        {
            throw new NotImplementedException();
        }

        // tarihe göre (yıl ve ay) articleları gruplar
        public async Task<IEnumerable<Article>> GetArticlesArchiveList(int year, int month)
        {
            await using var context = new DatabaseContext();

            return await context.Articles.Include(x => x.Category).Include(x => x.Comments)
                .Where(y => y.PublishDate.Year == year && y.PublishDate.Month == month).OrderByDescending(f => f.PublishDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Article>> GetArticlesOrderedDescByPublishDate()
        {
            await using var context = new DatabaseContext();

            return await context.Articles.Include(x => x.Category).OrderByDescending(x => x.PublishDate).ToListAsync();
        }

        public async Task<IEnumerable<ArticleReadDtoForDataTable>> GetArticlesForDataTable()
        {
            await using var context = new DatabaseContext();

            return await context.Articles.Include(x =>
               x.Category).Select(x => new ArticleReadDtoForDataTable()
               {
                   Title = x.Title,
                   Id = x.Id,
                   ViewCount = x.ViewCount,
                   PublishDate = x.PublishDate,
                   Picture = x.Picture,
                   CategoryName = x.Category.Name
               }).OrderByDescending(x => x.PublishDate).ToListAsync();
        }

        //article görüntülenme sayısını artırma
        public async Task ArticleViewCountUp(int id)
        {
            await using var context = new DatabaseContext();
            var article = context.Articles.Find(id);
            article.ViewCount += 1;

            await context.SaveChangesAsync();
        }

        public async Task ArticleCommentCountUp(int id)
        {
            await using var context = new DatabaseContext();

            var article = context.Articles.Find(id);

            if (article != null)
            {
                article.CommentCount += 1;

                await context.SaveChangesAsync();
            }

        }


        //public async Task<IEnumerable<Article>> GetArticlesArchieve()
        //{
        //    await using var context = new DatabaseContext();

        //    //return await context.Articles.GroupBy(x => new {x.PublishDate.Year, x.PublishDate.Month}).Select(y => new
        //    //{
        //    //    year = y.Key.Year,
        //    //    month = y.Key.Month,
        //    //    count = y.Count(),
        //    //    monthName = new DateTime(y.Key.Year, y.Key.Month, 1).ToString("MMMM",
        //    //        CultureInfo.CreateSpecificCulture("tr"))
        //    //}).ToListAsync();

        //    return await context.Articles.GroupBy(x => new {x.PublishDate.Year}).ToListAsync();
        //}

        public async Task<Article> GetArticleWithCategoryWithComment(int id)
        {
            await using var context = new DatabaseContext();
            return await context.Articles.Include(x => x.Category).Include(x => x.Comments).FirstOrDefaultAsync(x => x.Id == id);

        }



        public new async Task<int> Update(Article article)
        {
            await using var context = new DatabaseContext();
            //context.Entry(await context.Articles.FirstOrDefaultAsync(x=> x.Id==article.Id)).CurrentValues.SetValues(article);
            context.Attach(article);
            context.Entry(article).State = EntityState.Modified;
            return await context.SaveChangesAsync();
        }
    }
}