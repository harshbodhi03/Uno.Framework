using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Uno.Framework.Database;

namespace Uno.Framework
{
    public static class ContextSeed
	{
		public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
		{
			//Seed Roles
			await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));
			await roleManager.CreateAsync(new IdentityRole(Roles.Moderator.ToString()));
			await roleManager.CreateAsync(new IdentityRole(Roles.Administrator.ToString()));
		}

		public static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager, IConfiguration configuration)
		{
			//Seed Default User
			var defaultUser = new ApplicationUser
			{
				Email = configuration["Moderator:EmailConfig:Email"],
				UserName = configuration["Moderator:EmailConfig:Email"],
				FirstName = configuration["Moderator:EmailConfig:FirstName"],
				LastName = configuration["Moderator:EmailConfig:LastName"],
				EmailConfirmed = true,
				PhoneNumberConfirmed = true
			};
			if (userManager.Users.All(u => u.Id != defaultUser.Id))
			{
				var user = await userManager.FindByEmailAsync(defaultUser.Email);
				if (user == null)
				{
					var result = await userManager.CreateAsync(defaultUser, configuration["Moderator:EmailConfig:Password"]);
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
