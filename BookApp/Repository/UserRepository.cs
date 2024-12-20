using BookApp.Data;
using BookApp.DTO;
using BookApp.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookApp.Repository
{
    public class UserRepository : IUserServices
    {
        private readonly BookAppDbContext _context;
        private readonly IConfiguration _configuration;

        public UserRepository(BookAppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<bool> RegisterUserAsync(UserRegisterDto registerRequestDto)

        {
            var user = new User
            {
                Username = registerRequestDto.Username,
                Password = registerRequestDto.Password, // Hash this before saving
                
            };

            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<AuthDto> LoginAsync(UserLoginDto loginRequestDto)
        {
            var user = await _context.User
                .FirstOrDefaultAsync(u => u.Username == loginRequestDto.Username && u.Password == loginRequestDto.Password);

            if (user == null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new AuthDto
            {
                Token = tokenHandler.WriteToken(token)
            };
        }
    }
 }
