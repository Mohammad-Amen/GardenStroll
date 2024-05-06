using GardenStroll.Data;
using GardenStroll.DTOs;
using GardenStroll.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace GardenStroll.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly DataContext _context;
        public AccountController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username))
                return BadRequest("User is taken");

            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                Id = Guid.NewGuid(),
                Username = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return Ok(user);
        }

        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.Username == username);
        }

        [HttpDelete("deleteAllUsers")]
        public async Task<bool> DeleteUsers()
        {
            var allUsers = await _context.Users.ToListAsync();

            _context.RemoveRange(allUsers);

            return await _context.SaveChangesAsync() != 0;
        }
    }
}
