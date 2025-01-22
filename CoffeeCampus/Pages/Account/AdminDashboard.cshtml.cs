using Microsoft.EntityFrameworkCore;
using CoffeeCampus.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeCampus.Models.ViewModels;
using CoffeeCampus.Models;

namespace CoffeeCampus.Pages.Account
{
    [Authorize(Roles = "Admin")]
    public class AdminDashboardModel : PageModel
    {
        private readonly CoffeeCampusDbContext _context;

        public AdminDashboardModel(CoffeeCampusDbContext context) {
            _context = context;
        }

        // ViewModel for at holde påfyldningshistorikken
        public List<RefillViewModel> RefillHistory { get; set; } = new();

        // OnGet metode til at hente påfyldningshistorikken
        public async Task OnGetRefillHistoryAsync(string coffeemachineId = null, string userId = null, DateTime? startDate = null, DateTime? endDate = null) {
            var query = _context.Refills
                .Include(r => r.CoffeeMachine)  // Eager loading af CoffeeMachine relateret data
                .Include(r => r.User)          // Henter relaterede User data
                .AsQueryable();

            // Filtrering baseret på maskine, bruger og dato
            if (!string.IsNullOrEmpty(coffeemachineId))
                query = query.Where(r => r.CoffeeMachineID.ToString() == coffeemachineId);

            if (!string.IsNullOrEmpty(userId))
                query = query.Where(r => r.Responsible == userId);

            if (startDate.HasValue)
                query = query.Where(r => r.RefillDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(r => r.RefillDate <= endDate.Value);

            // Map til ViewModel
            RefillHistory = await query
                .Select(r => new RefillViewModel
                {
                    RefillID = r.RefillID,
                    DateTime = r.RefillDate,
                    RefillType = r.RefillType,
                    Responsible = r.Responsible,
                    CoffeeMachineId = r.CoffeeMachineID,
                })
                .ToListAsync();
        }
    }
}
