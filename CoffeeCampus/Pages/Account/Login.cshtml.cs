using CoffeeCampus.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace CoffeeCampus.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;


        public LoginModel(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            public string UserName { get; set; }


            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl ?? "/";
            ErrorMessage = TempData["ErrorMessage"] as string;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl ?? "/";

            if (!ModelState.IsValid)
            {
                return Page();
            }
            var result = await _signInManager.PasswordSignInAsync(
               Input.UserName,
               Input.Password,
               Input.RememberMe,
               lockoutOnFailure: false);

            if (result.Succeeded)
            {

                var user = await _signInManager.UserManager.FindByNameAsync(Input.UserName);

                if (user != null)
                {
                    if (await _signInManager.UserManager.IsInRoleAsync(user, "Admin"))
                    {
                        return LocalRedirect(returnUrl ?? "/Account/AdminDashboard");
                    }
                    else if (await _signInManager.UserManager.IsInRoleAsync(user, "User"))
                    {
                        return LocalRedirect(returnUrl ?? "/Account/UserDashboard");
                    }
                    else
                    {
                        return LocalRedirect(returnUrl ?? "/");  // Home page for others

                    }
                }

            }


            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Page();
        }

    }
}