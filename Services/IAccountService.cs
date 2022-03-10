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
        Task<Result> Verify(string id, string code);
        Task<Result> Login(LoginModel model);
        Task<Result> UpdateUser(UserModel model);
        Task<Result> SetUserMode(UserModeModel model);
        Task<bool> GetUserMode(ClaimsPrincipal userPrincipal);
        Task<Result> SetUserRole(RoleModel model);
        Task<Roles> GetUserRole(string id);
        Task<Result> DeleteUser(string id);
        Task<ApplicationUser> GetUser(string id);
        Task Logoff();
    }
}