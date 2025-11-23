using Microsoft.EntityFrameworkCore;
using WorkTimePanelFull.Domain.Entities;

namespace WorkTimePanelFull.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

        public DbSet<User> Users { get; set; } = null!;
    }
}
