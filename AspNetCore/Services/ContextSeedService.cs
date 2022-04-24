using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

using Uno.AspNetCore.Framework.Database;

namespace Uno.AspNetCore.Framework.Services
{
	public class ContextSeedService : IContextSeedService
	{
		private UserManager<ApplicationUser> _userManager;
		private RoleManager<IdentityRole> _roleManager;
		private readonly IConfiguration _configuration;
		bool _IsInitialized;

		public ContextSeedService(IConfiguration configuration)
		{
			_configuration = configuration;

			EmailSettings = new EmailSettings
			{
				Email = _configuration["Moderator:EmailSettings:Email"],
				FirstName = _configuration["Moderator:EmailSettings:FirstName"],
				LastName = _configuration["Moderator:EmailSettings:LastName"],
				Password = _configuration["Moderator:EmailSettings:Password"],
				Host = _configuration["Moderator:EmailSettings:Host"]
			};
		}

		public void Initialize(IServiceProvider serviceProvider)
		{
			if (_IsInitialized)
				return;

			_IsInitialized = true;
			_userManager = serviceProvider.GetService(typeof(UserManager<ApplicationUser>)) as UserManager<ApplicationUser>;
			_roleManager = serviceProvider.GetService(typeof(RoleManager<IdentityRole>)) as RoleManager<IdentityRole>;
		}

		public EmailSettings EmailSettings { get; private set; }

		public async Task SeedRolesAsync<T>() where T : Enum
		{
			if (_IsInitialized == false)
				throw new InvalidOperationException("Services are not initialized");

			//Seed Roles

			foreach (string enumType in Enum.GetNames(typeof(T)))
			{
				await _roleManager.CreateAsync(new IdentityRole(enumType));
			}
		}

		public async Task SeedAdminAsync(string adminUser = "Administrator")
		{
			if (_IsInitialized == false)
				throw new InvalidOperationException("Services are not initialized");

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
			if (_userManager.Users.All(u => u.Id != defaultUser.Id))
			{
				var user = await _userManager.FindByEmailAsync(defaultUser.Email);
				if (user == null)
				{
					var result = await _userManager.CreateAsync(defaultUser, EmailSettings.Password);
					if (result.Succeeded)
					{
						await _userManager.AddToRoleAsync(defaultUser, adminUser);
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
