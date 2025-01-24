using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CoffeeCampus.Data;
using CoffeeCampus.Models;

namespace CoffeeCampus.Pages.Users
{
    public class EditModel : PageModel
    {
        private readonly CoffeeCampusDbContext _context;

        [BindProperty]
        public User UpdatedUser { get; set; }

        public EditModel(CoffeeCampusDbContext context) { //Ionstruktor
            _context = context;
        }

        public IActionResult OnGet(string id) { //Metode for at finde den opdateret user
            UpdatedUser = _context.Users.FirstOrDefault(u => u.Id == id);

            if (UpdatedUser == null) { //Hvis user ikke fundet
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost() { //Metode til at opdatere user med FullName, Email, Department og en Emailconfirmed
            if (!ModelState.IsValid) {
                return Page();
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == UpdatedUser.Id);
            if (user == null) { //Hvis den opdaterede user ikke kunne findes
                return NotFound();
            }

            user.FullName = UpdatedUser.FullName;
            user.Email = UpdatedUser.Email;
            user.Department = UpdatedUser.Department;
            user.EmailConfirmed = UpdatedUser.EmailConfirmed;
            _context.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}
