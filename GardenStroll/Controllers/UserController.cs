using GardenStroll.Data;
using GardenStroll.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GardenStroll.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly DataContext _dataContext;

        public UserController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> Get()
        {
            return await _dataContext.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> getUser(Guid id)
        {
            return await _dataContext.Users.FindAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<int>> add(AppUser input)
        {
            var appUser = new AppUser
            {
                Id = Guid.NewGuid(),
                Username = input.Username
            };

            _dataContext.Users.Add(appUser);
            return await _dataContext.SaveChangesAsync();
        }
    }
}
