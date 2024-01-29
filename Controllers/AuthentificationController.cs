using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Mobilis.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Mobilis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public AuthenticationController(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User model)
        {
            try
            {
                // Check if the user already exists in the database
                if (_context.Users.Any(u => u.Username == model.Username))
                {
                    return BadRequest("Username already exists.");
                }

                // Validate password (you can add more complex validation rules)
                if (string.IsNullOrWhiteSpace(model.Password))
                {
                    return BadRequest("Password is required.");
                }

                // Hash password and add the user to the database
                
                model.Password = HashPassword(model.Password);
                model.CreatedAt = DateTime.UtcNow;
                model.UpdatedAt = DateTime.UtcNow;

                _context.Users.Add(model);
                _context.SaveChanges();

                return Ok("Registration successful.");
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred during registration.");
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User model)
        {
            // Validate model
            if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                return BadRequest("Email and password are required.");
            }

            // Find user by email and password
            var user = _context.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == HashPassword(model.Password));

            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            // Generate JWT token
            var token = GenerateJwtToken(user);

            return Ok(new { Token = token });
        }

        private string HashPassword(string password)
        {
            // Implement a secure password hashing algorithm (e.g., bcrypt) in a real-world scenario
            return password; // For simplicity, returning plain password for now
        }

        private string GenerateJwtToken(User user)
        {
            // Generate JWT token logic
            // Use the secret key from configuration

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = DateTime.UtcNow.AddHours(10),  // Token expiration time (adjust as needed)
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
