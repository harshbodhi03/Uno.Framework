using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
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

		public virtual void ConfigureServices(IServiceCollection services)
		{
			services.AddSession(options => {
				options.IdleTimeout = TimeSpan.FromMinutes(1);
			});

			services.AddSingleton<IEmailService, EmailService>();
			services.AddTransient<IAccountService, AccountService>();
			
			services.AddDbContext<BaseDbContext>(options =>
			{
				options.UseSqlServer(Configuration.GetConnectionString("SqlConnection"));
			});

			services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<BaseDbContext>().AddDefaultTokenProviders();
		}
	}
}
