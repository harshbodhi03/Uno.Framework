using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

using Uno.AspNetCore.Framework.Database;

namespace Uno.AspNetCore.Framework
{
    public static class ContextSeed
    {      
        public static EmailSettings EmailSettings { get; private set; }

        public static async Task Initialize(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var configuration = services.GetRequiredService<IConfiguration>();

            EmailSettings = new EmailSettings
            {
                Email = configuration["Moderator:EmailSettings:Email"],
                FirstName = configuration["Moderator:EmailSettings:FirstName"],
                LastName = configuration["Moderator:EmailSettings:LastName"],
                Password = configuration["Moderator:EmailSettings:Password"],
                Host = configuration["Moderator:EmailSettings:Host"]
            };

            await SeedRolesAsync(roleManager);
            await SeedAdminAsync(userManager, configuration);
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Moderator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Administrator.ToString()));
        }

        private static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                Email = EmailSettings.Email,
                UserName = EmailSettings.Email,
                FirstName = EmailSettings.FirstName,
                LastName = EmailSettings.LastName,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    var result = await userManager.CreateAsync(defaultUser, EmailSettings.Password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(defaultUser, Roles.Administrator.ToString());
                    }
                }
            }
        }
    }

    public enum Roles
    {
        Basic,
        Moderator,
        Administrator
    }
}
