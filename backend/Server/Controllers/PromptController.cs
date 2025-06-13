//using BL.Api;
//using BL.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace Server.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class PromptController : ControllerBase
//    {
//        private readonly IBLPrompt _promptServiec;

//        public PromptController(IBL BL)
//        {
//            _promptServiec = BL.Prompt;
//        }

//        [HttpGet("/user={id}")]
//        public ActionResult<List<BLPrompt>> GetPrompts(int id)
//        {
//            if (id <= 0)
//            {
//                return BadRequest("ID cannot be zero or negative.");
//            }
//           var v= _promptServiec.GetPromptsByUserIdAsync(id).ContinueWith(task =>
//            {
//                if (task.IsFaulted)
//                {
//                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving prompts.");
//                }
//                return Ok(task.Result);
//            });
//            return Ok(v.Result);


//        }
//        public class RequstBot
//        {
//            public string? Requst { get; set; }
//        }

//        [HttpPost]
//        public async Task<ActionResult<RequstBot>> CreatPrompts(BLPrompt bLPrompt)
//        {
//            if (bLPrompt == null)
//            {
//                return BadRequest("Prompt cannot be null");
//            }
//            var result = await _promptServiec.ProcessPromptAsync(bLPrompt);
//            return Ok(result);
//        }
//    }
//}
using BL.Api;
using BL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class PromptController : ControllerBase
    {
        private readonly IBLPrompt _promptService;

        public PromptController(IBLPrompt promptService)
        {
            _promptService = promptService;
        }

        // POST: api/Prompt
        [HttpPost]
        public async Task<IActionResult> CreatePrompt([FromBody] BLPrompt prompt)
        {
            var result = await _promptService.ProcessPromptAsync(prompt);
            return Ok(result);
        }

       // [Authorize(Roles = "Admin")]
        [HttpGet("admin/all")]
        public async Task<IActionResult> GetAllPromptsForAdmin()
        {
            try
            {
                Console.WriteLine("Getting prompts...");
                var prompts = await _promptService.GetAllAsync();
                Console.WriteLine($"Got {prompts.Count} prompts");

                return Ok(prompts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"שגיאה בשרת: {ex.Message}\n{ex.StackTrace}");
            }
        }



        // GET: api/Prompt/user/5
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetPromptsByUser(int userId)
        {
            var prompts = await _promptService.GetPromptsByUserIdAsync(userId);
            return Ok(prompts);
        }

        // GET: api/Prompt/user/5/category/2
        [HttpGet("user/{userId}/category/{categoryId}")]
        public async Task<IActionResult> GetPromptsByUserAndCategory(int userId, int categoryId)
        {
            var prompts = await _promptService.GetPromptsByUserIdAndCategotyAsync(userId, categoryId);
            return Ok(prompts);
        }

        //GET: api/Prompt/5
        [HttpGet("{id}")]
        public IActionResult GetPromptById(int id)
        {
            var prompt = _promptService.GetPromptByIdAsync(id);
            return Ok(prompt);
        }

        //PUT: api/Prompt/5
        [HttpPut("{id}")]
        public IActionResult UpdatePrompt(int id, [FromBody] BLPrompt prompt)
        {
            if (id != prompt.Id)
                return BadRequest("ID mismatch");

            _promptService.UpdateAsync(prompt);
            return NoContent();
        }

        // DELETE: api/Prompt/5
        [HttpDelete("{id}")]
        public IActionResult DeletePrompt(int id)
        {
            _promptService.DeleteAsync(id);
            return NoContent();
        }

        // POST: api/Prompt/validate
    }
}
