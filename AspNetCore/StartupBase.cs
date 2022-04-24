using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Uno.AspNetCore.Framework.Database;
using Uno.AspNetCore.Framework.Services;

namespace Uno.AspNetCore.Framework
{
	public class StartupBase
	{
		public StartupBase(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void Initialize<T>(IServiceCollection services, string connectionString) where T : IdentityDbContext<ApplicationUser>
		{
			services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

			services.AddSingleton<IContextSeedService, ContextSeedService>();
			services.AddTransient<IEmailService, EmailService>();
			services.AddTransient<IAccountService, AccountService>();

			services.AddDbContext<T>(options =>
			{
				if (string.IsNullOrWhiteSpace(Configuration.GetConnectionString(connectionString)) == false)
				{
					options.UseSqlServer(Configuration.GetConnectionString(connectionString));
				}
			});

			services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<T>().AddDefaultTokenProviders();
		}
	}
}
