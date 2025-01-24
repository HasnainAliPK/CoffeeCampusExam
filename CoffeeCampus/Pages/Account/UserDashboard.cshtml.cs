using CoffeeCampus.Data;
using CoffeeCampus.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace CoffeeCampus.Pages.Account
{
    [Authorize(Roles = "User")] //Attribut beskytter metoder og kontroller ved at kræve godkendelse 
    public class UserDashboardModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly CoffeeCampusDbContext _context;

        public string FullName { get; set; } //Admin Proppertys
        public string Email { get; set; }
        public string Department { get; set; }
        public string AdminName { get; set; }


        public UserDashboardModel(UserManager<User> userManager, CoffeeCampusDbContext context) {//Konstruktor
            _userManager = userManager;
            _context = context;
        }

        public async Task OnGetAsync() { //Metode til at finde useren

            var currentUser = await _userManager.GetUserAsync(User); // Venter med til at den har fundet useren



            if (currentUser != null) {
                FullName = currentUser.FullName;
                Email = currentUser.Email;
                Department = currentUser.Department;


                if (User.IsInRole("Admin")) //Hvis user er admin
                    AdminName = "You are the admin";
                else
                    AdminName = "This is the User Dashboard";



            }



        }
    }
}