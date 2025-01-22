using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CoffeeCampus.Data;
using CoffeeCampus.Models;

namespace CoffeeCampus.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly CoffeeCampusDbContext _context;

        [BindProperty]
        public User NewUser { get; set; }

        public CreateModel(CoffeeCampusDbContext context) {
            _context = context;
        }

        public IActionResult OnPost() {
            if (!ModelState.IsValid) {
                return Page();
            }

            _context.Users.Add(NewUser);
            _context.SaveChanges();

            return RedirectToPage("/Users/Create");
        }

    }
}
