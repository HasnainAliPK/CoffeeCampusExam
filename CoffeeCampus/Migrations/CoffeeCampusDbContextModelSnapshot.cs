using CoffeeCampus.Data;
using CoffeeCampus.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
namespace CoffeeCampus.Pages.Account
{
    [Authorize(Roles = "Admin")]
    public class UserManagementModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly CoffeeCampusDbContext _context;
        private readonly ILogger<UserManagementModel> _logger;


        public UserManagementModel(UserManager<User> userManager, CoffeeCampusDbContext context, ILogger<UserManagementModel> logger)
        {
            _userManager = userManager;
            _context = context;
            _logger = logger;
        }

        public IList<User> Users { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string Id { get; set; }

            [Required]
            [Display(Name = "Full Name")]
            public string FullName { get; set; }

            [Required]
            [Display(Name = "UserName")]
            public string UserName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Department")]
            public string Department { get; set; }

            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }
        }

        public async Task OnGetAsync()
        {
            var query = _userManager.Users.AsQueryable();


            if (!string.IsNullOrEmpty(SearchString))
            {
                query = query.Where(u => u.FullName.Contains(SearchString) || u.Email.Contains(SearchString) || u.UserName.Contains(SearchString));
            }

            Users = await query.ToListAsync();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                var user = new User
                {
                    FullName = Input.FullName,
                    Email = Input.Email,
                    UserName = Input.UserName,
                    Department = Input.Department
                };

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return Page();
                }
                TempData["SuccessMessage"] = "User created successfully!";
                return RedirectToPage("./UserManagement");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating user: {ex.Message}", ex);
                ModelState.AddModelError(string.Empty, $"Error creating user: {ex.Message}");
                return Page();

            }
        }

        public async Task<IActionResult> OnPostUpdateAsync(string userId)
        {
            if (!ModelState.IsValid) return Page();
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    TempData["ErrorMessage"] = "Could not find the user to update";
                    return RedirectToPage("./UserManagement");
                }


                user.FullName = Input.FullName;
                user.Email = Input.Email;
                user.UserName = Input.UserName;
                user.Department = Input.Department;


                if (!string.IsNullOrEmpty(Input.Password))
                {
                    var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var result = await _userManager.ResetPasswordAsync(user, resetToken, Input.Password);
                    if (!result.Succeeded)
                    {
                        TempData["ErrorMessage"] = "Could not update user password";
                        return RedirectToPage("./UserManagement");
                    }
                }


                var resultUpdate = await _userManager.UpdateAsync(user);
                if (!resultUpdate.Succeeded)
                {
                    TempData["ErrorMessage"] = "Could not update user";
                    return RedirectToPage("./UserManagement");
                }

                TempData["SuccessMessage"] = "User updated successfully!";
                return RedirectToPage("./UserManagement");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating user: {ex.Message}", ex);
                ModelState.AddModelError(string.Empty, $"Error updating user: {ex.Message}");
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    var result = await _userManager.DeleteAsync(user);
                    if (result.Succeeded)
                        TempData["SuccessMessage"] = "User deleted successfully!";
                    else
                    {
                        TempData["ErrorMessage"] = "Could not delete the user";
                    }
                    return RedirectToPage("./UserManagement");


                }
                else
                {
                    TempData["ErrorMessage"] = "Could not find the user to delete";
                    return RedirectToPage("./UserManagement");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting user: {ex.Message}", ex);
                ModelState.AddModelError(string.Empty, $"Error deleting user: {ex.Message}");
                return Page();

            }
        }

        public async Task<IActionResult> OnPostResetPasswordAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                    if (resetToken == null)
                    {
                        TempData["ErrorMessage"] = "Could not generate reset password token";
                        return RedirectToPage("./UserManagement");
                    }
                    var password = "UserDefaultPassword123!"; //Replace with a random password generator for better security.
                    var result = await _userManager.ResetPasswordAsync(user, resetToken, password);

                    if (result.Succeeded)
                    {
                        TempData["SuccessMessage"] = "Password reset to default successfully!";
                        return RedirectToPage("./UserManagement");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Could not reset password";
                        return RedirectToPage("./UserManagement");
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Could not find the user to reset password for";
                    return RedirectToPage("./UserManagement");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error resetting user password: {ex.Message}", ex);
                ModelState.AddModelError(string.Empty, $"Error resetting user password: {ex.Message}");
                return Page();

            }
        }
    }
}