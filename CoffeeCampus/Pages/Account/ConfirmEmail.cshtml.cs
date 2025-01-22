using CoffeeCampus.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace CoffeeCampus.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<User> _userManager;

        public string ErrorMessage { get; set; } // Show an error message

        public ConfirmEmailModel(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(string userId, string token)
        {

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                ErrorMessage = "Invalid verification link.";
                return RedirectToPage("./Login");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ErrorMessage = "Invalid verification link.";
                return RedirectToPage("./Login");
            }


            var result = await _userManager.ConfirmEmailAsync(user, token);


            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Email confirmed.";
                return RedirectToPage("./Login");
            }
            else
            {
                ErrorMessage = "Could not confirm email. Please try again later.";
                return RedirectToPage("./Login");
            }
        }


    }
}