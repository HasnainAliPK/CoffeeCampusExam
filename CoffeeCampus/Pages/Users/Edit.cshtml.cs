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

        public EditModel(CoffeeCampusDbContext context) {
            _context = context;
        }

        public IActionResult OnGet(string id) {
            UpdatedUser = _context.Users.FirstOrDefault(u => u.Id == id);

            if (UpdatedUser == null) {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost() {
            if (!ModelState.IsValid) {
                return Page();
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == UpdatedUser.Id);
            if (user == null) {
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
