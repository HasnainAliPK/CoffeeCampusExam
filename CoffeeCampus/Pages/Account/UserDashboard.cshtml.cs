using CoffeeCampus.Data;
using CoffeeCampus.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace CoffeeCampus.Pages.Account
{
    [Authorize(Roles = "User")] // Assuming "User" role for non-admin users
    public class UserDashboardModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly CoffeeCampusDbContext _context;

        public string FullName { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public string AdminName { get; set; }


        public UserDashboardModel(UserManager<User> userManager, CoffeeCampusDbContext context) {
            _userManager = userManager;
            _context = context;
        }

        public async Task OnGetAsync() {

            var currentUser = await _userManager.GetUserAsync(User); // This fetches the logged in user



            if (currentUser != null) {
                FullName = currentUser.FullName;
                Email = currentUser.Email;
                Department = currentUser.Department;


                if (User.IsInRole("Admin"))
                    AdminName = "You are the admin";
                else
                    AdminName = "This is the User Dashboard";



            }



        }
    }
}