using Microsoft.AspNetCore.Mvc;
using ProjectTrackerAPI.Models;
using ProjectTrackerAPI.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace ProjectTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ProjectDbContext _context;

        public LoginController(ProjectDbContext context)
        {
            _context = context;
        }

        [HttpGet("all-users")]
        public IActionResult GetAllUsers()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }

        [HttpPost]
public async Task<IActionResult> LoginUser([FromBody] User user)
{
    try
    {
        Console.WriteLine("Received login attempt:");
        Console.WriteLine("Username from user input: " + user.Username);
        Console.WriteLine("Password from user input: " + user.Password);

        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);

        if (existingUser != null)
        {
            Console.WriteLine("Username from DB: " + existingUser.Username);
            Console.WriteLine("Password from DB: " + existingUser.Password);

            if (existingUser.Password == user.Password)
            {
                return Ok(new { message = "Login successfully" });
            }
            else
            {
                return BadRequest(new { message = "Invalid Username or Password" });
            }
        }

        return BadRequest(new { message = "Invalid Username or Password" });
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Login error: {ex.Message}");
        return StatusCode(500, $"Internal server error: {ex.Message}");
    }
}

    }
}
