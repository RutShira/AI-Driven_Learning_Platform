using BL.Api;
using BL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly IBLSubCategory _subcategoryServiec;

        public SubCategoryController(IBL BL)
        {
            _subcategoryServiec = BL.SubCategory;
        }
        [HttpGet]
        public ActionResult<List<BLSubCategory>> GetAll()
        {
            try
            {
                var subcategories = _subcategoryServiec.GetAll();
                return Ok(subcategories);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving subcategories: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("/category={id}")]
        public ActionResult<List<BLSubCategory>> GetAllByCategory(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID cannot be zero or negative.");
            }
            List<BLSubCategory> subcategory = _subcategoryServiec.GetAllByCategory(id);
            if (subcategory == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }
            return Ok(subcategory);
        }
        [HttpGet("{id}")]
        public ActionResult<BLSubCategory> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID cannot be zero or negative.");
            }
            var subcategory = _subcategoryServiec.Read(id);
            if (subcategory == null)
            {
                return NotFound($"SubCategory with ID {id} not found.");
            }
            return Ok(subcategory);
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
                _subcategoryServiec.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(knfEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while deleting the subcategory: {ex.Message}");
            }
        }
    
        [HttpPost]
        public ActionResult<BLSubCategory> Create(BLSubCategory subcategory)
        {
            if (subcategory == null)
            {
                return BadRequest("SubCategory cannot be null.");
            }
            try
            {
                _subcategoryServiec.Create(subcategory);
                return CreatedAtAction(nameof(GetById), new { id = subcategory.SubCategoryId }, subcategory);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while creating the subcategory: {ex.Message}");
            }
        }
        [HttpPut]
        public ActionResult<BLSubCategory> Update(BLSubCategory subcategory)
        {
            if (subcategory == null)
            {
                return BadRequest("SubCategory cannot be null.");
            }
            try
            {
                _subcategoryServiec.Update(subcategory);
                return Ok(subcategory);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while updating the subcategory: {ex.Message}");
            }
        }
    }
}
