using GardenStroll.Entities;

namespace GardenStroll.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
