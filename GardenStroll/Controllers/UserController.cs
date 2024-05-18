using GardenStroll.Data;
using GardenStroll.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GardenStroll.Controllers
{
    [Authorize]
    public class UserController : BaseApiController
    {
        private readonly DataContext _dataContext;

        public UserController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetAllUsers()
        {
            return await _dataContext.Users.Include(user => user.Photos).ToListAsync();
        }

        [HttpGet("GetUser/{id}")]
        public async Task<ActionResult<AppUser>> getUser(Guid id)
        {
            return await _dataContext.Users.FindAsync(id);
        }
    }
}
