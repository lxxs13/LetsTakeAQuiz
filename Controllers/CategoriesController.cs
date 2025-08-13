using GeekQuiz.Interfaces;
using GeekQuiz.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;

namespace GeekQuiz.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategory _categoryService;

        public CategoriesController(ICategory categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var result = _categoryService.GetCategories();

            if (!result.Status)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpGet]
        public IActionResult GetCategoriesByStatus() {
            var result = _categoryService.GetCategoriesByStatus();

            if (!result.Status)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpPost]
        public IActionResult CreateCategory(CategoriesDTO dto)
        {
            var result = _categoryService.CreateCategory(dto);

            if (!result.Status)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] CategoriesDTO dto)
        {
            var result = _categoryService.UpdateCategory(dto);

            if (!result.Status)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpPut]
        public IActionResult UpdateStatus([FromBody] int[] ids)
        {
            var result = _categoryService.UpdateCategoryStatus(ids);

            if (!result.Status)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var result = _categoryService.DeleteCategory(id);

            if (!result.Status)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

    }
}
