using AADx.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace AADx.TodoApi.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options): base(options)
        {
        }

        public DbSet<UserProfile> Users { get; set; }
    }
}