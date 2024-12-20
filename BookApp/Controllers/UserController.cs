using BookApp.Data;
using BookApp.DTO;
using BookApp.Model;
using BookApp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly BookAppDbContext _context;
        private readonly IConfiguration _configuration;

        public UserController(BookAppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // POST: api/User/register
        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            if (!ModelState.IsValid)
            /*if (_context.Users.Any(u => u.Username == user.Username))*/
            {
                return BadRequest("Username already exists.");
            }

            _context.User.Add(user);
            _context.SaveChanges();

            return Ok("User registered successfully.");
        }

        // POST: api/User/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto loginDto)
        {
            var user = _context.User.FirstOrDefault(u => u.Username == loginDto.Username && u.Password == loginDto.Password);

            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            var token = GenerateJwtToken(user);

            return Ok(new AuthDto
            {
                Token = token,
                Username = user.Username
                
            });
        }
        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                
            };

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}