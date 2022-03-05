using System.ComponentModel.DataAnnotations;

namespace Uno.AspNetCore.Framework.Models
{
	public class RegisterModel
	{
		[EmailAddress(ErrorMessage = "E-mail not valid")]
		[Required(ErrorMessage = "Field cannot be empty")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Field cannot be empty")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Field cannot be empty")]
		[Compare(nameof(Password), ErrorMessage = "Confirm password do not match")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
	}
}