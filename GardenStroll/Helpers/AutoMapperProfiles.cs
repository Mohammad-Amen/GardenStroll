using AutoMapper;
using GardenStroll.DTOs;
using GardenStroll.Entities;

namespace GardenStroll.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>();
            CreateMap<Photo, PhotoDto>();
        }
    }
}
