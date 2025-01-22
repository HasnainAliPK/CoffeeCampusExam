using Microsoft.AspNetCore.Identity;

namespace CoffeeCampus.Models
{
    public class User : IdentityUser // Removed  , IApplicationUser
    {
        public string FullName { get; set; }
       
        public string Department { get; set; }
        // public string AdminId { get; set; } // Remove AdminId

        // Keep the bool props if needed for your application.
       
        public bool PhoneNumberConfirmed { get; set; } //Add PhoneNumberConfirmed
        public bool TwoFactorEnabled { get; set; }
        public bool LockoutEnabled { get; set; }
    }
}