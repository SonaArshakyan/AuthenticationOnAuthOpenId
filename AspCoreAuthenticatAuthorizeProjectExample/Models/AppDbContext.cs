using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreAuthenticatAuthorizeProjectExample.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options)
        {
        }
        public DbSet<LoginModel> LoginModels { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 2,
                    FavColor = "yellow",
                    UserName = "Sona",
                    GoogleId = "104018143982611409017",
                    Role = "Admin",
                    Password = "Sonajan_21"
                });
        }
    }
}
