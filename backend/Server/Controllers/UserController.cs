using BL.Api;
using BL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Specialized;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class UserController : ControllerBase
    {
        private readonly IBLUser _userService;

        public UserController(IBL BL)
        {
            _userService = BL.User;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult GetById([FromBody] LoginModel loginModel)
        {
            Console.WriteLine("in controller");
            if (loginModel.Password <= 0)
            {
                return BadRequest("ID cannot be zero.");
            }

            var user = _userService.Read(loginModel.Password);
            if (user.Name != loginModel.Username)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [Authorize(Policy = "ManagerOnly")]
        
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_userService.GetAll());
        }
        [HttpPut]
        
        [Authorize]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public ActionResult<BLUser> Update(BLUser user)
        {
            if (user == null)
            {
                return BadRequest("User cannot be null.");
            }
            _userService.Update(user);
            return user;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public ActionResult<BLUser> Create(BLUser user)
        {
            if (user == null)
            {
                return BadRequest("User cannot be null.");
            }
            _userService.Create(user);
            return CreatedAtAction(nameof(GetById), new { id = user.UserId }, user);
        }
    }
}
