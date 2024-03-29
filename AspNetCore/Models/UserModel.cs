﻿using System.ComponentModel.DataAnnotations;

namespace Uno.AspNetCore.Framework.Models
{
	public class UserModel
	{
		public string Id { get; set; }

		[Required(ErrorMessage = "Field cannot be empty")]
		public string FirstName { get; set; }

		public string LastName { get; set; }

		[EmailAddress(ErrorMessage = "Not a valid email format.")]
		public string Email { get; set; }

		[Phone(ErrorMessage = "Not a valid phone number format.")]
		public string PhoneNumber { get; set; }

		public string Role { get; set; }

		public string[] Roles { get; set; }= {
			Services.Roles.Basic.ToString(),
			Services.Roles.Administrator.ToString(),
			Services.Roles.Moderator.ToString()
		};

		public byte[] ProfilePicture { get; set; }
	}
}
