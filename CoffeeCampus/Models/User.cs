using Microsoft.AspNetCore.Identity;

namespace CoffeeCampus.Models
{
    public class User : IdentityUser //User rollefordeling
    {
        public string FullName { get; set; }
       
        public string Department { get; set; }
        public bool PhoneNumberConfirmed { get; set; } 
        public bool TwoFactorEnabled { get; set; }
        public bool LockoutEnabled { get; set; }
    }
}