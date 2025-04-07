using Microsoft.AspNetCore.Mvc;
using ProjectTrackerAPI.Models;
using ProjectTrackerAPI.Data;
using ProjectTrackerAPI.Services; // ✅ Make sure this is here to find EmailUsername
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjectTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResetPasswordController : ControllerBase // ✅ Controller class name fixed
    {
        private readonly EmailPasswordLink _emailService;
        private readonly ProjectDbContext _context;

        // ✅ Constructor name must match class name
        public ResetPasswordController(EmailPasswordLink emailService, ProjectDbContext context)
        {
            _emailService = emailService;
            _context = context;
        }

        [HttpPost("request-password-reset")]
        public async Task<IActionResult> RegisterUser([FromBody] User user)
        {
            try
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
                if (existingUser != null)
                {
                     var resetToken = new ResetToken(_context).GeneratePasswordResetToken(user.Id);
                     var resetLink = $"{Request.Scheme}://{Request.Host}/reset-password?token={resetToken}";
                    var emailSent = await _emailService.SendPasswordResetEmail(user.Email, resetLink);
                    if (emailSent)
                    {
                        return Ok(new { message = "Link to reset your password has been sent to your email. Please check it." });
                    }
                    else
                    {
                        return BadRequest(new { message = "Something went wrong. Please try again." });
                    }
                }
                else
                {
                    return BadRequest(new { message = "There is no registered account associated with the email you entered." });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Username recovery error: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("reset-password")]
public async Task<IActionResult> ResetPassword([FromBody] PasswordResetModel model)
{
    var user = await _context.Users.FirstOrDefaultAsync(u => u.ResetPasswordToken == model.Token);

    if (user == null || user.ResetPasswordTokenExpiration < DateTime.Now)
    {
        return BadRequest(new { message = "Invalid or expired reset token." });
    }

    user.Password = model.NewPassword; // Make sure to hash the password before saving it.
    user.ResetPasswordToken = null; // Clear the token after reset
    user.ResetPasswordTokenExpiration = null;

    await _context.SaveChangesAsync();
    return Ok(new { message = "Password has been reset successfully." });
}
    }
}
