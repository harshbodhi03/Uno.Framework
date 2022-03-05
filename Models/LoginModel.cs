using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Uno.AspNetCore.Framework.Models
{
    public class LoginModel
	{
		[EmailAddress(ErrorMessage = "E-mail not valid")]
		[Required(ErrorMessage = "Field cannot be empty")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Field cannot be empty")]
		public string Password { get; set; }

		[DisplayName("Remember me?")]
		public bool RememberMe { get; set; }
	}
}
