using AVB.ApiBlog.DataAccess.Interfaces;
using AVB.ApiBlog.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AVB.ApiBlog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryDal _categoryDal;

        public CategoriesController(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        // GET api/<CategoryController>
        [HttpGet]
        public async Task<ActionResult<Category>> GetAllCategories()
        {
            System.Threading.Thread.Sleep(1500);
            var categories = await _categoryDal.GetAllOrderById();

            if (categories == null)
                return NotFound();

            return Ok(categories);
        }

        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            var category = await _categoryDal.GetById(id);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public async Task<IActionResult> PostCategory(Category category)
        {
            await _categoryDal.Add(category);
            return CreatedAtAction("GetCategoryById", new { id = category.Id }, category);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Article>> DeleteArticle(int id)
        {
            var category = await _categoryDal.GetById(id);
            if (category == null)
            {
                return NotFound();
            }

            await _categoryDal.Remove(category);

            return Ok(category);
        }
    }
}