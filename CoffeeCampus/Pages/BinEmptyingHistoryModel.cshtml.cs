using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeeCampus.Models;
using CoffeeCampus.Models.ViewModels;
using CoffeeCampus.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeCampus.Pages.Account
{
    [Authorize(Roles = "Admin")]
    public class BinEmptyingHistoryModel : PageModel
    {
        private readonly CoffeeCampusDbContext _context;

        public BinEmptyingHistoryModel(CoffeeCampusDbContext context)
        {
            _context = context;
        }

        public List<BinEmptyingViewModel>
    EmptyingHistory
        { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(string coffeemachineId = null, string userId = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = _context.Set<BinEmptying>() // Use Dbset directly
                 .Include(e => e.CoffeeMachine)
                 .AsQueryable();

            // Filtering
            if (!string.IsNullOrEmpty(coffeemachineId))
                query = query.Where(e => e.CoffeeMachineID.ToString() == coffeemachineId);

            if (!string.IsNullOrEmpty(userId))
                query = query.Where(e => e.Responsible == userId);

            if (startDate.HasValue)
                query = query.Where(e => e.DateTime >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(e => e.DateTime <= endDate.Value);


            // Map to ViewModel
            EmptyingHistory = await query
                .Select(e => new BinEmptyingViewModel
                {
                    EmptyingID = e.EmptyingID, // Access the correct property
                    DateTime = e.DateTime,
                    Responsible = e.Responsible,
                    CoffeeMachineID = e.CoffeeMachineID
                })
            .ToListAsync();

            return Page();
        }
    }
}