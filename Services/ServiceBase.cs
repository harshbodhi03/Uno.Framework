using Uno.AspNetCore.Framework.Database;

namespace Uno.AspNetCore.Framework.Services
{
    public class ServiceBase<T>
	{
		protected T mDbContext;
		public ServiceBase(T dbContext)
		{
			mDbContext = dbContext;
		}
	}
}
