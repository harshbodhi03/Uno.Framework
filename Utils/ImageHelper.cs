using Microsoft.AspNetCore.Http;
using System.IO;

namespace Uno.AspNetCore.Framework.Utils
{
	public class ImageHelper
	{
		public static byte[] GetImageBytes(IFormFile imageFile)
		{
			if (imageFile == null)
				return null;

			using MemoryStream stream = new MemoryStream();
			imageFile.CopyToAsync(stream).Wait();
			return stream.ToArray();
		}
	}
}
