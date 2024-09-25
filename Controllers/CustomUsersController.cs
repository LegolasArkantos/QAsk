using CrudApp.Data;
using CrudApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using CrudApp.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CrudApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomUsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private const string StaticSalt = "YourStaticSalt"; 

        public CustomUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/CustomUsers/Register
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                return BadRequest("Passwords do not match.");
            }

            var existingUser = await _context.CustomUsers.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (existingUser != null)
            {
                return BadRequest("Email is already in use.");
            }

            var user = new CustomUser
            {
                Email = model.Email,
                PasswordHash = HashPassword(model.Password)

            };

            _context.CustomUsers.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully.");
        }

        private string HashPassword(string password)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: System.Text.Encoding.UTF8.GetBytes(StaticSalt), 
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }


        // POST: api/CustomUsers/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var user = await _context.CustomUsers.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null || user.PasswordHash != HashPassword(model.Password))
            {
                return Unauthorized("Invalid credentials.");
            }

            // Here you can set up a session or return a token (JWT) for authentication
            return Ok("Login successful.");
        }

        


    }
}
