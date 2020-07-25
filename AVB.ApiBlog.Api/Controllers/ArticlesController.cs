using AutoMapper;
using AVB.ApiBlog.DataAccess.Interfaces;
using AVB.ApiBlog.Entities.DtoModels;
using AVB.ApiBlog.Entities.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;

namespace AVB.ApiBlog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleDal _articleDal;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;

        public ArticlesController(IArticleDal articleDal, IMapper mapper, IWebHostEnvironment environment)
        {
            _articleDal = articleDal;
            _mapper = mapper;
            _environment = environment;
        }

        public Tuple<IEnumerable<Article>, int> ArticlesPagination(IEnumerable<Article> articles, int page, int pageSize)

        {
            var totalCount = articles.Count();

            var articlesLast = articles.Skip(pageSize * (page - 1)).Take(pageSize).ToList();

            return new Tuple<IEnumerable<Article>, int>(articlesLast, totalCount);
        }

        [HttpGet]
        [Route("ArticleViewCountUp/{id}")]
        public async Task<IActionResult> ArticleViewCountUp(int? id)
        {
            if (id != null) await _articleDal.ArticleViewCountUp(id.Value);

            return Ok();
        }

        // DELETE: api/Articles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var article = await _articleDal.GetById(id);
            if (article == null)
            {
                return NotFound();
            }

            await _articleDal.Remove(article);

            return Ok();
        }

        // GET: api/Articles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticle(int id)
        {
            System.Threading.Thread.Sleep(2000);
            var article = await _articleDal.GetArticleWithCategoryWithComment(id);

            if (article == null)
            {
                return NotFound();
            }

            var articleReadDto = _mapper.Map<ArticleReadDto>(article);

            var result = new
            {
                article = articleReadDto
            };

            return Ok(article);
        }

        // GET: api/Articles/1/5
        [HttpGet("{page}/{pageSize}")]
        public async Task<ActionResult<List<Article>>> GetArticleForPagination(int page = 1, int pageSize = 5)
        {
            System.Threading.Thread.Sleep(2000);
            List<Article> articles = await _articleDal.GetArticlesWithCategoryWithComment(page, pageSize);

            if (articles == null)
            {
                return NotFound();
            }

            var totalCount = articles.Count();

            var articlesLast = articles.Skip(pageSize * (page - 1)).Take(pageSize).ToList();

            //var result = ArticlesPagination(articles, page, pageSize);

            //IEnumerable<ArticleReadDto> articleReadDto = _mapper.Map<IEnumerable<ArticleReadDto>>(articlesLast);

            var result = new
            {
                TotalCount = totalCount,
                Articles = articlesLast
            };

            return Ok(result);
        }

        // GET: api/Articles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticles()
        {
            _environment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            return await _articleDal.GetAll();
        }

        [HttpGet]
        [Route("GetArticlesArchive")]
        public async Task<ActionResult> GetArticlesArchive()
        {
            System.Threading.Thread.Sleep(1000);
            var archives = await _articleDal.GetAll();

            var archivesGrouped = archives.GroupBy(x => new { x.PublishDate.Year, x.PublishDate.Month }).Select(y => new
            {
                year = y.Key.Year,
                month = y.Key.Month,
                count = y.Count(),
                monthName = new DateTime(y.Key.Year, y.Key.Month, 1).ToString("MMMM",
                      CultureInfo.CreateSpecificCulture("tr"))
            });
            return Ok(archivesGrouped);
        }

        // tarihi seçilen makaleleri gruplayıp getirir
        [HttpGet]
        [Route("GetArticlesArchiveList/{year}/{month}/{page}/{pageSize}")]
        public async Task<ActionResult> GetArticlesArchiveList(int year, int month, int page = 1, int pageSize = 5)
        {
            System.Threading.Thread.Sleep(1500);
            var articlesArchiveList = await _articleDal.GetArticlesArchiveList(year, month);

            var articlesArchiveListPaginated = ArticlesPagination(articlesArchiveList, page, pageSize);

            var articlesResult = new
            {
                Articles = articlesArchiveListPaginated.Item1,
                TotalCount = articlesArchiveListPaginated.Item2
            };

            return Ok(articlesResult);
        }

        [HttpGet]
        [Route("GetArticlesByCategory/{categoryId}/{page}/{pageSize}")]
        public async Task<ActionResult<Article>> GetArticlesByCategory(int categoryId, int page = 1, int pageSize = 5)
        {
            System.Threading.Thread.Sleep(2000);
            var articles = await _articleDal.GetArticlesByCategory(categoryId);

            if (articles == null)
                return NotFound();

            var articlesResult = ArticlesPagination(articles, page, pageSize);

            var result = new
            {
                TotalCount = articlesResult.Item2,
                Articles = articlesResult.Item1,
            };
            return Ok(result);
        }

        [HttpGet]
        [Route("GetArticlesByMostView")]
        public async Task<ActionResult<List<Article>>> GetArticlesByMostView()
        {
            System.Threading.Thread.Sleep(2000);
            var articlesByMostView = await _articleDal.GetArticlesByMostView();
            var articles = articlesByMostView.Select(x => new
            {
                Title = x.Title,
                Id = x.Id,
                ViewCount = x.ViewCount
            });

            return Ok(articles);
        }

        // GET: api/Articles/GetArticlesForDataTable
        [HttpGet]
        [Route("GetArticlesForDataTable")]
        public async Task<IEnumerable<ArticleReadDtoForDataTable>> GetArticlesForDataTable()
        {
            return await _articleDal.GetArticlesForDataTable();
        }

        [HttpGet]
        [Route("GetArticlesOrderedDescByPublishDate")]
        public async Task<IActionResult> GetArticlesOrderedDescByPublishDate()
        {
            var articlesOrderedDescByPublishDate = await _articleDal.GetArticlesOrderedDescByPublishDate();

            if (articlesOrderedDescByPublishDate != null)
                return Ok(articlesOrderedDescByPublishDate);

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Article>> PostArticle(Article article)
        {
            System.Threading.Thread.Sleep(1000);
            article.CategoryId = article.Category.Id;
            article.Category = null;
            article.ViewCount = 0;
            article.PublishDate = DateTime.Now;
            await _articleDal.Add(article);

            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Article>> PutArticle(int id,ArticlePutDto articlePutDto)
        {
            
            Article checkArticle = await _articleDal.GetById(id);
            if (checkArticle == null)
            {
                return NotFound();
            }

            var date = checkArticle.PublishDate;


            //auto mapper eşleşmeyen propertileri nulla dönüştürdüğü için geçici çözüm

            
            var passArticle  =  _mapper.Map<Article>(articlePutDto);
            //checkArticle = article;

            checkArticle.Title = passArticle.Title;
            checkArticle.ContentMain = passArticle.ContentMain;
            checkArticle.ContentSummary = passArticle.ContentSummary;
            checkArticle.CategoryId = passArticle.CategoryId;

            checkArticle.PublishDate = date;

            var p = await _articleDal.Update(checkArticle);
            return Ok(p);
        }
        
        [HttpPost]
        [Route("SaveArticlePicture")]
        public async Task<IActionResult> SaveArticlePicture(IFormFile picture)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(picture.FileName);

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/articlePictures", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await picture.CopyToAsync(stream);
            };
            var result = new
            {
                path = "https://" + Request.Host + "/articlePictures/" + fileName
            };

            return Ok(result);
        }

        [HttpGet]
        [Route("SearchArticles/{searchText}/{page}/{pageSize}")]
        public async Task<ActionResult<List<Article>>> SearchArticles(string searchText, int page = 1, int pageSize = 5)
        {
            System.Threading.Thread.Sleep(1500);
            var articles = await _articleDal.SearchArticles(searchText);

            var articlesPagination = ArticlesPagination(articles, page, pageSize);

            var result = new
            {
                Articles = articlesPagination.Item1,
                TotalCount = articlesPagination.Item2
            };

            return Ok(result);
        }
    }
}