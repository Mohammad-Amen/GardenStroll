using GardenStroll.Data;
using GardenStroll.DTOs;
using GardenStroll.Entities;
using GardenStroll.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace GardenStroll.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto registerDto)
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

            var userInfo = new UserDto
            {
                Username = registerDto.Username,
                Token = _tokenService.CreateToken(user)
            };

            return userInfo;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await GetUser(loginDto.Username);

            if (user == null)
                return Unauthorized("invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            if (!computedHash.SequenceEqual(user.PasswordHash))
                return Unauthorized("invalid password");

            var userInfo = new UserDto
            {
                Username = loginDto.Username,
                Token = _tokenService.CreateToken(user)
            };

            return userInfo;
        }


        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.Username == username);
        }

        private async Task<AppUser> GetUser(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
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
