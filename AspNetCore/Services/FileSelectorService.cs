using Microsoft.AspNetCore.Http;
using System.IO;

namespace Uno.AspNetCore.Framework.Services
{
	public class FileSelectorService : IFileSelectorService
    {
        /// <summary>
        /// Save selected file with supplied file name. File will be overwritten if already exists.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="file"></param>
        public void Save(string fileName, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return;
            
            using (var stream = new FileStream(fileName, FileMode.Create))
            {
                file.CopyTo(stream);
            }
        }
    }
}
