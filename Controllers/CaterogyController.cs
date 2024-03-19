using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using RESTwebAPI.Models;
using RESTwebAPI.Services;

namespace RESTwebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            var category = await _categoryService.GetAllCategorysAsync();
            return Ok(category);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            var category = await _categoryService.GetCategoryAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category.Data);
        }
        [HttpPost]
        public async Task<ActionResult<Category>> Post(Category category)
        {
            var newCategory = await _categoryService.AddCategoryAsync(category);
            return CreatedAtAction(nameof(Get), new { id = newCategory.Data.CategoryId }, newCategory.Data);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            var updatedCategory = await _categoryService.UpdateCategoryAsync(id,category);
            if (updatedCategory.Data == null)
            {
                return NotFound();
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedCategory = await _categoryService.DeleteCategoryAsync(id);
            if (deletedCategory.Data == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
