using AutoMapper;
using AutoMapper.QueryableExtensions;
using GardenStroll.Data;
using GardenStroll.DTOs;
using GardenStroll.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GardenStroll.Controllers
{
    [AllowAnonymous]
    public class UserController : BaseApiController
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public UserController(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetAllUsers()
        {
            var users =  await _dataContext.Users.Include(user => user.Photos).ToListAsync();

            var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);

            return Ok(usersToReturn);
        }

        [HttpGet("GetUser/{id}")]
        public async Task<ActionResult<AppUser>> getUser(Guid id)
        {
            var user = await _dataContext.Users.Where(x => x.Id == id)
                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();

            return Ok(user);
        }
    }
}
