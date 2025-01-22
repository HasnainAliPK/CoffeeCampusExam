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
    [Authorize]
    public class RegisterCleaningModel : PageModel
    {
        private readonly CoffeeCampusDbContext _context;
        private readonly UserManager<User> _userManager; // Use UserManager<User>


        public RegisterCleaningModel(CoffeeCampusDbContext context, UserManager<User> userManager) // inject UserManager<User>
        {
            _context = context;
            _userManager = userManager;
        }


        [BindProperty]
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



        public async Task OnGetAsync()
        {
            CoffeeMachines = await _context.CoffeeMachines.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid) return Page(); // Return if model state is not valid.

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