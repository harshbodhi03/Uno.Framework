using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Uno.AspNetCore.Framework.Database;

namespace Uno.AspNetCore.Framework.Services
{
    public class ServiceBase<T> where T : IdentityDbContext<ApplicationUser>
    {
        protected T mDbContext;
        public ServiceBase(T dbContext)
        {
            mDbContext = dbContext;
        }
    }
}
