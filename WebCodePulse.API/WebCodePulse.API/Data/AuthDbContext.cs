using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebCodePulse.API.Models.Enum;

namespace WebCodePulse.API.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "28d65a5b-a7db-4850-b380-83591f7d7531";
            var writerRoleId = "9740f16c-24a1-4224-a7be-1bb00b7c6892";

            // Create Reader and Writer Role
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = readerRoleId,
                    Name = RoleEnum.Reader.ToString(),
                    NormalizedName = RoleEnum.Reader.ToString().ToUpper(),
                    ConcurrencyStamp = readerRoleId
                },
                new IdentityRole()
                {
                    Id = writerRoleId,
                    Name = RoleEnum.Writer.ToString(),
                    NormalizedName = RoleEnum.Writer.ToString().ToUpper(),
                    ConcurrencyStamp = writerRoleId
                }
            };

            // Seed the roles
            builder.Entity<IdentityRole>().HasData(roles);


            // Create an Admin User
            var adminUserId = "edc267ec-d43c-4e3b-8108-a1a1f819906d";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "manash.dholey@example.com",
                Email = "manash.dholey@example.com",
                NormalizedEmail = "manash.dholey@example.com".ToUpper(),
                NormalizedUserName = "manash.dholey@example.com".ToUpper()
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@123");

            builder.Entity<IdentityUser>().HasData(admin);

            // Give Roles To Admin

            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = adminUserId,
                    RoleId = readerRoleId
                },
                new()
                {
                    UserId = adminUserId,
                    RoleId = writerRoleId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }
    }
}
