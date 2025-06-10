using BL.Api;
using BL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromptController : ControllerBase
    {
        private readonly IBLPrompt _promptServiec;

        public PromptController(IBL BL)
        {
            _promptServiec = BL.Prompt;
        }

        [HttpGet("/user={id}")]
        public ActionResult<List<BLPrompt>> GetPrompts(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID cannot be zero or negative.");
            }
           var v= _promptServiec.GetPromptsByUserIdAsync(id).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving prompts.");
                }
                return Ok(task.Result);
            });
            return Ok(v.Result);


        }

        [HttpPost]
        public async Task<IActionResult> PutPrompts(BLPrompt bLPrompt)
        {
            if (bLPrompt == null)
            {
                return BadRequest("Prompt cannot be null");
            }
            var result = await _promptServiec.ProcessPromptAsync(bLPrompt);
            return Ok(result);
        }
    }
}
