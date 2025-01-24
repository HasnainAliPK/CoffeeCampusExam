using CoffeeCampus.Data;
using CoffeeCampus.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeCampus.Pages.Account
{
    [Authorize] //Attribut beskytter metoder og kontroller ved at kr�ve godkendelse 
    public class RegisterCleaningModel : PageModel 
    {
        private readonly CoffeeCampusDbContext _context;
        private readonly UserManager<User> _userManager; // CRUD


        public RegisterCleaningModel(CoffeeCampusDbContext context, UserManager<User> userManager) //Konstruktor 
        {
            _context = context;
            _userManager = userManager;
        }


        [BindProperty] //Forbinder data fra Database til UserManager
        public InputModel Input { get; set; }

        public List<CoffeeMachine> CoffeeMachines { get; set; }


        public class InputModel
        {
            [Required]
            [Display(Name = "Machine")]
            public int CoffeeMachineID { get; set; }

            [Required]
            [DataType(DataType.DateTime)]
            [Display(Name = "Cleaning Date and Time")]
            public DateTime CleaningDateTime { get; set; }
        }



        public async Task OnGetAsync() //Metode for at vise alle maskinerne i en liste
        {
            CoffeeMachines = await _context.CoffeeMachines.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid) return Page(); 

            var user = await _userManager.GetUserAsync(User); // Get the user

            if (user == null)
            {
                return Forbid(); //Or return NotFound
            }

            var cleaningRecord = new MachineCleaning
            {

                CoffeeMachineID = Input.CoffeeMachineID,
                CleaningDateTime = Input.CleaningDateTime,
                ResponsiblePersonId = user.Id // Set User ID
            };

            _context.MachineCleanings.Add(cleaningRecord);

            await _context.SaveChangesAsync(); // Save to DB


            return RedirectToPage("CleaningHistory"); // redirect to history
        }
    }
}