using GardenStroll.Entities;
using GardenStroll.Extension;

namespace GardenStroll.DTOs
{
    public class MemberDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public int Age { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LasActive { get; set; } = DateTime.UtcNow;
        public string Gender { get; set; }
        public string Introduction { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public List<PhotoDto> Photos { get; set; } = new List<PhotoDto>();
    }
}
