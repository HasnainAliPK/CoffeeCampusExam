using CoffeeCampus.Data;
using CoffeeCampus.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeCampus.Pages.Account
{
    [Authorize]
    public class CleaningHistoryModel : PageModel
    {
        private readonly CoffeeCampusDbContext _context;

        public CleaningHistoryModel(CoffeeCampusDbContext context) {
            _context = context;
        }

        public IList<MachineCleaning> CleaningRecords { get; set; }

        public async Task OnGetAsync() {
            CleaningRecords = await _context.MachineCleanings
                .Include(c => c.CoffeeMachine)
                 .Include(c => c.ResponsiblePerson) // Updated to User
                .OrderByDescending(c => c.CleaningDateTime)
                .ToListAsync();
        }
    }
}