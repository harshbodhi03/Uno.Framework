using Microsoft.AspNetCore.Identity;

namespace Uno.AspNetCore.Framework.Database
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsUserMode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] ProfilePicture { get; set; }
    }
}
