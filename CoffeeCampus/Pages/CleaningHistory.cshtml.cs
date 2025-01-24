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
    [Authorize] //Attribut beskytter metoder og kontroller ved at kr�ve godkendelse 
    public class CleaningHistoryModel : PageModel
    {
        private readonly CoffeeCampusDbContext _context;

        public CleaningHistoryModel(CoffeeCampusDbContext context) { //Konstruktur
            _context = context;
        }

        public IList<MachineCleaning> CleaningRecords { get; set; }

        public async Task OnGetAsync() { //Metode til at f� en liste af reng�ring
            CleaningRecords = await _context.MachineCleanings
                .Include(c => c.CoffeeMachine) //Inkluder alle Kaffemaskienr
                 .Include(c => c.ResponsiblePerson) // Inkluder alle ResposiblePerson
                .OrderByDescending(c => c.CleaningDateTime) //Filtrer efter seneste f�rst
                .ToListAsync();
        }
    }
}