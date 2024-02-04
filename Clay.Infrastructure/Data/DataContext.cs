using Clay.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clay.Infrastructure.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions options): base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Lock> Locks { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<LockRole> LockRoles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}
