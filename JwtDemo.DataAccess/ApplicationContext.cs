using JwtDemo.DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JwtDemo.DataAccess
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }

        // FluentApi
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // configurate one-to-one
            builder.Entity<User>()
                // User має одне UserInfo
                .HasOne(u => u.UserInfo)
                // UserInfo є тільки в одного юзера
                .WithOne(t => t.User)
                // і зовнішнім ключем буде UserInfo.Id
                .HasForeignKey<UserInfo>(a => a.Id);

            base.OnModelCreating(builder);
        }
    }
}
