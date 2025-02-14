using Microsoft.AspNetCore.Identity;
using Rommelmarkten.Api.Domain.Entities;
using Rommelmarkten.Api.Domain.Users;
using Rommelmarkten.Api.Infrastructure.Identity;
using Rommelmarkten.Api.Infrastructure.Services;
using System.Security.Claims;

namespace Rommelmarkten.Api.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            var administratorRole = new IdentityRole("Administrator");
            var userRole = new IdentityRole("User");

            var avatarGenerator = new AvatarGenerator();

            var roles = new[] { administratorRole, userRole };

            foreach(var role in roles)
            {
                if (roleManager.Roles.All(r => r.Name != role.Name))
                {
                    await roleManager.CreateAsync(role);
                }
            }

            var adminUser = new ApplicationUser { Id = "admin-id", UserName = "admin", Email = "administrator@localhost" };
            var userA = new ApplicationUser { Id = "thelma-id", UserName = "thelma", Email = "thelma@localhost" };
            var userB = new ApplicationUser { Id = "louise-id", UserName = "louise", Email = "louise@localhost" };
            var userC = new ApplicationUser { Id = "jimmy-id", UserName = "jimmy", Email = "jimmy@localhost" };

            var users = new[] { adminUser, userA, userB, userC };

            foreach (var user in users)
            {
                if (userManager.Users.All(u => u.UserName != user.UserName))
                {
                    await userManager.CreateAsync(user, "Seedpassword1!");

                    var avatar = await avatarGenerator.GenerateAvatar(user);

                    var base64 = Convert.ToBase64String(avatar.Content);

                    var profile = new UserProfile()
                    {
                        Avatar = avatar,
                        Consented = true,
                        UserId = user.Id,
                    };
                    context.UserProfiles.Add(profile);
                    await context.SaveChangesAsync();
                }
            }

            //add claims to role level
            await roleManager.AddClaimAsync(administratorRole, new Claim(Application.Common.Security.ClaimTypes.IsAdmin, "true"));
            await roleManager.AddClaimAsync(userRole, new Claim(Application.Common.Security.ClaimTypes.IsUser, "true"));

            //add users to roles
            await userManager.AddToRolesAsync(adminUser, new[] { administratorRole.Name });
            await userManager.AddToRolesAsync(userA, new[] { userRole.Name });
            await userManager.AddToRolesAsync(userB, new[] { userRole.Name });
            await userManager.AddToRolesAsync(userC, new[] { userRole.Name });
        }

        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
            if (!context.Categories.Any())
            {
                var mainCategories = new[]
                {
                    new Category{ Name = "Bakery", Icon = "bakery" },
                    new Category{ Name = "Fish", Icon = "fish" },
                    new Category{ Name = "Meat and Poultry", Icon = "meat" },
                    new Category{ Name = "Frozen", Icon = "frozen" },
                    new Category{ Name = "Pets", Icon = "pets" },
                    new Category{ Name = "Other", Icon = "other" },
                };
                foreach(var category in mainCategories)
                {
                    category.Created = DateTime.Now.AddDays(-1);
                    category.CreatedBy = "admin-id";
                }

                await context.Categories.AddRangeAsync(mainCategories);
                await context.SaveChangesWithoutAutoAuditables();
            }

            if (!context.ShoppingLists.Any())
            {
                var thelmasList = new ShoppingList
                {
                    Title = "Shopping",
                    Color = null,
                    Created = DateTime.Now.AddDays(-1),
                    CreatedBy = "thelma-id",
                    Items =
                    {
                        new ListItem { Title = "Apples", Done = true },
                        new ListItem { Title = "Milk", Done = true },
                        new ListItem { Title = "Bread", Done = true },
                        new ListItem { Title = "Toilet paper" },
                        new ListItem { Title = "Pasta" },
                        new ListItem { Title = "Tissues" },
                        new ListItem { Title = "Tuna" },
                        new ListItem { Title = "Water" }
                    }
                };

                context.ShoppingLists.Add(thelmasList);
                await context.SaveChangesWithoutAutoAuditables();

                var louiseAssociate = new ListAssociate
                {
                    AssociatedOn = DateTime.Now,
                    AssociateId = "louise-id",
                    ListId = thelmasList.Id
                };

                context.ListAssociates.Add(louiseAssociate);
                await context.SaveChangesAsync();
            }
        }
    }
}
