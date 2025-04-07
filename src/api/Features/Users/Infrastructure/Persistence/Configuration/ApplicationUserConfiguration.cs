using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Features.Users.Domain;

namespace Rommelmarkten.Api.Features.Users.Infrastructure.Persistence.Configuration
{
    public static class ApplicationIdentityExtensions
    {
        public static void ConfigureApplicationIdentityModels(this ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.Logins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<ApplicationRole>(b =>
            {
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();
            });
        }
    }

    //public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    //{
    //    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    //    {
    //        Console.WriteLine("Configuring ApplicationUser...");

    //        // Each User can have many UserClaims
    //        builder.HasMany(e => e.Claims)
    //            .WithOne()
    //            .HasForeignKey(uc => uc.UserId)
    //            .IsRequired();

    //        // Each User can have many UserLogins
    //        builder.HasMany(e => e.Logins)
    //            .WithOne()
    //            .HasForeignKey(ul => ul.UserId)
    //            .IsRequired();

    //        // Each User can have many UserTokens
    //        builder.HasMany(e => e.Tokens)
    //            .WithOne()
    //            .HasForeignKey(ut => ut.UserId)
    //            .IsRequired();

    //        // Each User can have many entries in the UserRole join table
    //        builder.HasMany(e => e.UserRoles)
    //            .WithOne()
    //            .HasForeignKey(ur => ur.UserId)
    //            .IsRequired();
    //    }
    //}

    //public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    //{
    //    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    //    {
    //        Console.WriteLine("Configuring ApplicationRole...");

    //        // Each Role can have many entries in the UserRole join table
    //        builder.HasMany(e => e.UserRoles)
    //            .WithOne(e => e.Role)
    //            .HasForeignKey(ur => ur.RoleId)
    //            .IsRequired();

    //        // Each Role can have many associated RoleClaims
    //        builder.HasMany(e => e.RoleClaims)
    //            .WithOne(e => e.Role)
    //            .HasForeignKey(rc => rc.RoleId)
    //            .IsRequired();
    //    }
    //}
}
