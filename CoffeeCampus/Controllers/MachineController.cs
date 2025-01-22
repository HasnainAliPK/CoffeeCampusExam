using CoffeeCampus.Data;
using CoffeeCampus.Models;
using CoffeeCampus.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CoffeeCampus.Controllers
{
    [Authorize]  //Godkendelse for Admin eller Bruger og deres rettigheder

    public class MachineController : Controller //MC arver egenskaber fra C
    {
        private readonly MachineCleaningService _machineCleaningService;
        private readonly CoffeeMachineStatisticsService _coffeeMachineStatisticsService;
        private readonly CoffeeCampusDbContext _context; // Tilføj denne linje

        public MachineController(
            MachineCleaningService machineCleaningService,
            CoffeeMachineStatisticsService coffeeMachineStatisticsService,
            CoffeeCampusDbContext context) // Tilføj context som parameter
        {
            _machineCleaningService = machineCleaningService;
            _coffeeMachineStatisticsService = coffeeMachineStatisticsService;
            _context = context; // Tildel til feltet
        }

        [HttpPost]
        public IActionResult Service(int machineId, string username) {
            // Brug username til at finde User fra databasen
            var responsiblePerson = _context.Users.FirstOrDefault(u => u.UserName == username);

            if (responsiblePerson == null) {
                // Håndter fejl (fx vis en fejlbesked eller log fejlen)
                return BadRequest("Brugeren blev ikke fundet.");
            }

            _machineCleaningService.AddMachineCleaningLog(machineId, responsiblePerson);
            return RedirectToAction("Service", new { machineId });
        }
    }
}
