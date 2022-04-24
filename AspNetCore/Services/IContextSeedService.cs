using System;
using System.Threading.Tasks;

namespace Uno.AspNetCore.Framework.Services
{
	public interface IContextSeedService
	{
		EmailSettings EmailSettings { get; }

		void Initialize(IServiceProvider serviceProvider);
			
		Task SeedRolesAsync<T>() where T : Enum;

		Task SeedAdminAsync(string adminUser = "Administrator");
	}
}
