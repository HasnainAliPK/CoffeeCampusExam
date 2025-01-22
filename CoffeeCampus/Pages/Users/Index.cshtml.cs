using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CoffeeCampus.Data;
using CoffeeCampus.Models;
using System.Linq;

namespace CoffeeCampus.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly CoffeeCampusDbContext _context;

        public List<User> Users { get; set; }

        public IndexModel(CoffeeCampusDbContext context) {
            _context = context;
        }

        public void OnGet() {
            Users = _context.Users.ToList();
        }
    }
}
