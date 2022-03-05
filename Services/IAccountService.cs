using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Uno.AspNetCore.Framework.Database;
using Uno.AspNetCore.Framework.Data;
using Uno.AspNetCore.Framework.Models;

namespace Uno.AspNetCore.Framework.Services
{
    public interface IAccountService
	{
		IEnumerable<ApplicationUser> GetUsers();
		Task<RegisterResult> Register(RegisterModel model);
		Task<Result> Verify(string userId, string code);
		Task<Result> Login(LoginModel model);
		Task<Result> UpdateUser(string userId, UserModel model);
		Task<Result> SetUserMode(string userId, bool userMode);
		Task<bool> GetUserMode(ClaimsPrincipal userPrincipal);
		Task<Result> SetUserRole(string userId, Roles newRole);
		Task<Roles> GetUserRole(string userId);
		Task<Result> DeleteUser(string id);
		Task<ApplicationUser> GetUser(string id);
		Task Logoff();
	}
}
