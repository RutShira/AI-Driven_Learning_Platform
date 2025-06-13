//using BL.Api;
//using Dal.Services;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using Microsoft.AspNetCore.Authorization;

//namespace Server.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthController : ControllerBase
//    {
//        private readonly IBLUser _userService;
//        private readonly IConfiguration _configuration;

//        public AuthController(IBL BL, IConfiguration configuration)
//        {
//            _userService = BL.User;
//            _configuration = configuration;
//        }

//        [HttpPost("login")]
//        public IActionResult Login([FromBody] LoginModel userModel)
//        {
//            var user = _userService.Read(userModel.Password);

//            if (user == null)
//            {
//                throw new Exception("user undefine");
//            }

//            if (user != null && userModel.Password == user.UserId)
//            {
//                var tokenHandler = new JwtSecurityTokenHandler();
//                var tokenDescriptor = new SecurityTokenDescriptor
//                {
//                    Subject = new ClaimsIdentity(new[] { new Claim("id", user.UserId.ToString()) }),
//                    Expires = DateTime.UtcNow.AddHours(1),
//                    Issuer = _configuration["Jwt:Issuer"],
//                    SigningCredentials = new SigningCredentials(
//                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
//                        SecurityAlgorithms.HmacSha256Signature)
//                };
//                var token = tokenHandler.CreateToken(tokenDescriptor);
//                return Ok(new { Token = tokenHandler.WriteToken(token) });
//            }
//            return Unauthorized();
//        }

//        [Authorize]
//        [HttpGet("me")]
//        public IActionResult GetMe()
//        {
//            var userIdClaim = User.FindFirst("id")?.Value;

//            if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
//            {
//                return BadRequest("Invalid user ID.");
//            }

//            var user = _userService.Read(userId);
//            if (user == null)
//                return NotFound();

//            return Ok(user);
//        }
//    }
//}
using BL.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Phone { get; set; }
    }


    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var adminSection = _configuration.GetSection("AdminCredentials");
        var adminUsername = adminSection["Username"];
        var adminPhone = adminSection["Phone"];

        if (request.Username == adminUsername && request.Phone == adminPhone)
        {
            var token = GenerateJwtToken(request.Username);
            return Ok(new { token });
        }

        return Unauthorized("שם משתמש או סיסמה שגויים");
    }

    private string GenerateJwtToken(string username)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
