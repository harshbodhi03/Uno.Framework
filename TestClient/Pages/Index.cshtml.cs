using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uno.AspNetCore.Framework.Services;

namespace TestClient.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;

		public IndexModel(ILogger<IndexModel> logger, IContextSeedService contextSeedService)
		{
			_logger = logger;
		}

		public void OnGet()
		{

		}
	}
}
