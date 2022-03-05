using Uno.Framework.Database;

namespace Uno.Framework.Services
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
