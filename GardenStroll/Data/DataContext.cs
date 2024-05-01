using GardenStroll.Entities;
using Microsoft.EntityFrameworkCore;

namespace GardenStroll.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppUser> Users { get; set; }
    }
}
