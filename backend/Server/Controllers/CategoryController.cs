using BL.Api;
using BL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IBLCategory _categoryServiec;

        public CategoryController(IBL BL)
        {
            _categoryServiec = BL.Category;
        }
        [HttpGet]
        public ActionResult<List<BLCategory>> GetAll()
        {
            try
            {
                var categories = _categoryServiec.GetAll();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving categories: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult<BLCategory> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID cannot be zero or negative.");
            }
            var category = _categoryServiec.Read(id);
            if (category == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }
            return Ok(category);
        }
        [HttpPost]
        public ActionResult<BLCategory> Create(BLCategory category)
        {
            if (category == null)
            {
                return BadRequest("Category cannot be null.");
            }
            try
            {
                _categoryServiec.Create(category);
                return CreatedAtAction(nameof(GetById), new { id = category.CategoryId }, category);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while creating the category: {ex.Message}");
            }
        }
        [HttpPut]
        public ActionResult<BLCategory> Update(BLCategory category)
        {
            if (category == null)
            {
                return BadRequest("Category cannot be null.");
            }
            try
            {
                _categoryServiec.Update(category);
                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while updating the category: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID cannot be zero or negative.");
            }
            try
            {
                _categoryServiec.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(knfEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while deleting the category: {ex.Message}");
            }
        }
    }
}
