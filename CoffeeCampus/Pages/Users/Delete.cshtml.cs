using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CoffeeCampus.Data;
using CoffeeCampus.Models;

namespace CoffeeCampus.Pages.Users
{
    public class DeleteModel : PageModel
    {
        private readonly CoffeeCampusDbContext _context;

        [BindProperty]
        public User UserToDelete { get; set; }

        public DeleteModel(CoffeeCampusDbContext context) {
            _context = context;
        }

        public IActionResult OnGet(string id) {
            UserToDelete = _context.Users.FirstOrDefault(u => u.Id == id);

            if (UserToDelete == null) {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost() {
            var user = _context.Users.FirstOrDefault(u => u.Id == UserToDelete.Id);
            if (user != null) {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }

            return RedirectToPage("Index");
        }
    }
}
