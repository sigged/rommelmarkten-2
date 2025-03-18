using Microsoft.AspNetCore.Identity;
using Rommelmarkten.Api.Features.ShoppingLists.Domain;
using Rommelmarkten.Api.Features.ShoppingLists.Infrastructure.Persistence;
using Rommelmarkten.Api.Features.Users.Domain;
using Rommelmarkten.Api.Features.Users.Infrastructure.Services;
using System.Security.Claims;

namespace Rommelmarkten.Api.WebApi.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, UsersDbContext context)
        {
            var administratorRole = new IdentityRole("Administrator");
            var userRole = new IdentityRole("User");

            var avatarGenerator = new AvatarGenerator();

            var roles = new[] { administratorRole, userRole };

            foreach (var role in roles)
            {
                if (roleManager.Roles.All(r => r.Name != role.Name))
                {
                    await roleManager.CreateAsync(role);
                }
            }

            var adminUser = new ApplicationUser { Id = "admin-id", UserName = "administrator@localhost", Email = "administrator@localhost", EmailConfirmed = true };
            var userA = new ApplicationUser { Id = "thelma-id", UserName = "thelma@localhost", Email = "thelma@localhost", EmailConfirmed = true };
            var userB = new ApplicationUser { Id = "louise-id", UserName = "louise@localhost", Email = "louise@localhost", EmailConfirmed = true };
            var userC = new ApplicationUser { Id = "jimmy-id", UserName = "jimmy@localhost", Email = "jimmy@localhost", EmailConfirmed = true };

            var users = new[] { adminUser, userA, userB, userC };

            foreach (var user in users)
            {
                if (userManager.Users.All(u => u.UserName != user.UserName))
                {
                    var result = await userManager.CreateAsync(user, "Seedpassword1!");

                    //var avatar = await avatarGenerator.GenerateAvatar(user);

                    //var base64 = Convert.ToBase64String(avatar.Content);

                    var profile = new UserProfile()
                    {
                        //Avatar = avatar,
                        Name = user.UserName ?? "New User",
                        Consented = true,
                        OwnedBy = user.Id,
                    };
                    context.Set<UserProfile>().Add(profile);
                    await context.SaveChangesAsync();
                }
            }

            //add claims to role level
            await roleManager.AddClaimAsync(administratorRole, new Claim(Common.Application.Security.ClaimTypes.IsAdmin, "true"));
            await roleManager.AddClaimAsync(userRole, new Claim(Common.Application.Security.ClaimTypes.IsUser, "true"));

            //add users to roles
            await userManager.AddToRolesAsync(adminUser, new[] { administratorRole.Name ?? "" });
            await userManager.AddToRolesAsync(userA, new[] { userRole.Name ?? "" });
            await userManager.AddToRolesAsync(userB, new[] { userRole.Name ?? "" });
            await userManager.AddToRolesAsync(userC, new[] { userRole.Name ?? "" });
        }

        public static async Task SeedSampleDataAsync(ShoppingListsDbContext context)
        {
            // Seed, if necessary
            if (!context.Set<Category>().Any())
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
                foreach (var category in mainCategories)
                {
                    category.Created = DateTime.Now.AddDays(-1);
                    category.CreatedBy = "admin-id";
                }

                await context.Set<Category>().AddRangeAsync(mainCategories);
                await context.SaveChangesWithoutAutoAuditables();
            }

            if (!context.Set<ShoppingList>().Any())
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

                context.Set<ShoppingList>().Add(thelmasList);
                await context.SaveChangesWithoutAutoAuditables();

                var louiseAssociate = new ListAssociate
                {
                    AssociatedOn = DateTime.Now,
                    AssociateId = "louise-id",
                    ListId = thelmasList.Id
                };

                context.Set<ListAssociate>().Add(louiseAssociate);
                await context.SaveChangesAsync();
            }
        }
    }
}
