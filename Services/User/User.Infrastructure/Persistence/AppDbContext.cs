
using User.Core.Entities;
using Microsoft.Extensions.Configuration;

using MongoDB.Driver;
using Microsoft.EntityFrameworkCore;

namespace User.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Users> Users { get; set; }
    }
}
