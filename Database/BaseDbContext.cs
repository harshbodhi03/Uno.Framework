using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Uno.Framework.Database
{
    public class BaseDbContext : IdentityDbContext<ApplicationUser>
    {
        public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options)
        {
        }

        protected BaseDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
