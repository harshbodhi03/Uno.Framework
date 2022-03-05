using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Uno.AspNetCore.Framework.Data;
using Uno.AspNetCore.Framework.Database;
using Uno.AspNetCore.Framework.Models;

namespace Uno.AspNetCore.Framework.Services
{
    public class AccountService : ServiceBase<BaseDbContext>, IAccountService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountService(BaseDbContext context, UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager) : base(context)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public async Task<RegisterResult> Register(RegisterModel model)
		{
			var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
			var result = await _userManager.CreateAsync(user, model.Password);
			string token = null;
			RegisterResult registerResult = new RegisterResult();

			if (result.Succeeded)
			{
				await _userManager.AddToRoleAsync(user, Roles.Basic.ToString());
				token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
				registerResult.Succeeded = result.Succeeded;
			}
			else
			{
				registerResult.Error = result.Errors.First().Description;
			}

			registerResult.Id = user.Id;
			registerResult.Token = token;
			return registerResult;
		}

		public async Task<Result> Verify(string userId, string code)
		{
			var verifyResult = new Result();
			var user = await _userManager.FindByIdAsync(userId);
			if (user == null)
			{
				verifyResult.Error = $"Invalid user id {userId}.";
				return verifyResult;
			}

			var result = await _userManager.ConfirmEmailAsync(user, code);
			verifyResult.Succeeded = result.Succeeded;
			if (verifyResult.Succeeded == false)
				verifyResult.Error = result.Errors.First().Description;

			return verifyResult;
		}

		public async Task<Result> Login(LoginModel model)
		{
			var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
			return new Result() { Succeeded = result.Succeeded };
		}

		public async Task Logoff()
		{
			await _signInManager.SignOutAsync();
		}

		public async Task<Result> UpdateUser(string userId, UserModel model)
		{
			if (userId == null)
				throw new ArgumentNullException(nameof(userId));

			if (model == null)
				throw new ArgumentNullException(nameof(model));

			var verifyResult = new Result();

			var user = await _userManager.FindByIdAsync(userId);
			if (user != null)
			{
				user.FirstName = model.FirstName;
				user.LastName = model.LastName;
				user.PhoneNumber = model.PhoneNumber;

				var result = await _userManager.UpdateAsync(user);

				verifyResult.Succeeded = result.Succeeded;
				if (verifyResult.Succeeded == false)
					verifyResult.Error = result.Errors.First().Description;
			}

			return verifyResult;
		}

		public async Task<Result> SetUserMode(string userId, bool userMode)
		{
			if (userId == null)
				throw new ArgumentNullException(nameof(userId));

			var verifyResult = new Result();

			var user = await _userManager.FindByIdAsync(userId);
			if (user != null)
			{
				user.IsUserMode = userMode;
				var result = await _userManager.UpdateAsync(user);

				verifyResult.Succeeded = result.Succeeded;
				if (verifyResult.Succeeded == false)
					verifyResult.Error = result.Errors.First().Description;
			}

			return verifyResult;
		}

		public async Task<Result> SetUserRole(string userId, Roles newRole)
		{
			if (userId == null)
				throw new ArgumentNullException(nameof(userId));

			var verifyResult = new Result();

			var user = await _userManager.FindByIdAsync(userId);
			if (user != null)
			{
				var roles = await _userManager.GetRolesAsync(user);
				foreach (var role in roles)
				{
					await _userManager.RemoveFromRoleAsync(user, role);
				}

				var result = await _userManager.AddToRoleAsync(user, newRole.ToString());
				verifyResult.Succeeded = result.Succeeded;
				if (verifyResult.Succeeded == false)
					verifyResult.Error = result.Errors.First().Description;
			}

			return verifyResult;
		}

		public async Task<Roles> GetUserRole(string userId)
		{
			if (userId == null)
				throw new ArgumentNullException(nameof(userId));

			var user = await _userManager.FindByIdAsync(userId);
			if (user != null)
			{
				var roles = await _userManager.GetRolesAsync(user);
				return (Roles)Enum.Parse(typeof(Roles), roles[0]);
			}

			return Roles.Basic;
		}

		public async Task<bool> GetUserMode(ClaimsPrincipal userPrincipal)
		{
			if (userPrincipal == null)
				throw new ArgumentNullException(nameof(userPrincipal));

			var user = await _userManager.GetUserAsync(userPrincipal);
			if (user != null)
				return user.IsUserMode;

			return false;
		}

		public IEnumerable<ApplicationUser> GetUsers()
		{
			return _signInManager.UserManager.Users.ToList();
		}

		public async Task<Result> DeleteUser(string id)
		{
			if (id == null)
				throw new ArgumentNullException(nameof(id));

			var user = await _userManager.FindByIdAsync(id);
			var verifyResult = new Result();
			if (user == null)
				return verifyResult;

			var result = await _signInManager.UserManager.DeleteAsync(user);
			verifyResult.Succeeded = result.Succeeded;
			if (verifyResult.Succeeded == false)
				verifyResult.Error = result.Errors.First().Description;

			return verifyResult;
		}

		public async Task<ApplicationUser> GetUser(string id)
		{
			if (id == null)
				throw new ArgumentNullException(nameof(id));

			var user = await _userManager.FindByIdAsync(id);
			return user;
		}
	}
}
