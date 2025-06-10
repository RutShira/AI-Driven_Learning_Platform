using BL.Api;
using Dal.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;


namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IBLUser _userService;
        private readonly IConfiguration _configuration; // Add this field

        public AuthController(IBL BL, IConfiguration configuration) // Update constructor to accept IConfiguration
        {
            _userService = BL.User;
            _configuration = configuration; // Initialize the configuration field
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel userModel)
        {
            var user = _userService.Read(userModel.Password);

            if (user != null && userModel.Password==user.UserId)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("id", user.UserId.ToString()) }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    Issuer = _configuration["Jwt:Issuer"], // Use the injected configuration
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return Ok(new { Token = tokenHandler.WriteToken(token) });
            }
            return Unauthorized();
        }
    }
}
