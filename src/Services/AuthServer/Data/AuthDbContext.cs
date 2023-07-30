using Auth.Models;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Auth.Data
{
    public class AuthDbContext : ApiAuthorizationDbContext<AuthUser>
    {
        private static readonly string adminGuid = Guid.NewGuid().ToString();
        private static readonly string adminRoleGuid = Guid.NewGuid().ToString();

        public AuthDbContext(
            DbContextOptions dbContextOptions,
            IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(dbContextOptions, operationalStoreOptions)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            SeedUsers(builder);
            SeedRoles(builder);
            SeedUserRoles(builder);
        }

        private static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Id = adminRoleGuid,
                    Name = Data.UserRoles.Admin,
                    ConcurrencyStamp = "1",
                    NormalizedName = Data.UserRoles.Admin.ToUpper()
                },
                new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = Data.UserRoles.Engineer,
                    ConcurrencyStamp = "2",
                    NormalizedName = Data.UserRoles.Engineer.ToUpper()
                },
                new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = Data.UserRoles.Manager,
                    ConcurrencyStamp = "3",
                    NormalizedName = Data.UserRoles.Manager.ToUpper()
                }
                );
        }

        private static void SeedUserRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>()
                {
                    RoleId = adminRoleGuid,
                    UserId = adminGuid
                }
                );
        }

        private static void SeedUsers(ModelBuilder builder)
        {
            var userAdmin = "admin@admin";
            var userPass = "Qwe123!@#";

            AuthUser user = new()
            {
                Id = adminGuid,
                LockoutEnabled = false,

                Email = userAdmin,
                NormalizedEmail = userAdmin.ToUpper(),
                FullUserName = "Admin Adminovich",
                Role = Data.UserRoles.Admin,
                PhoneNumber = "1234567890",
                Password = userPass,
                UserName = userAdmin,
                NormalizedUserName = userAdmin.ToUpper(),
            };

            PasswordHasher<AuthUser> passwordHasher = new();
            user.PasswordHash = passwordHasher.HashPassword(user, userPass);

            builder.Entity<AuthUser>().HasData(user);
        }
    }
}