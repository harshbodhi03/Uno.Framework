using Microsoft.AspNetCore.Http;

namespace Uno.AspNetCore.Framework.Services
{
	public interface IFileSelectorService
	{
		void Save(string fileName, IFormFile file);
	}
}