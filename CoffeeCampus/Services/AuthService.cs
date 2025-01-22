using CoffeeCampus.Data;
using CoffeeCampus.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

public class AuthService
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly CoffeeCampusDbContext _context;

    public AuthService(SignInManager<User> signInManager,
                       UserManager<User> userManager,
                       CoffeeCampusDbContext context)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _context = context;
    }

    public async Task<bool> LoginAsync(string userName, string password) // Use username instead of email
    {
        var result = await _signInManager.PasswordSignInAsync(
            userName, // use username directly
             password,
             isPersistent: false,
             lockoutOnFailure: true);

        return result.Succeeded;
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<IdentityResult> RegisterAdminAsync(User admin, string password)
    {
        var result = await _userManager.CreateAsync(admin, password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(admin, "Admin");
        }
        return result;
    }


    public async Task<IdentityResult> CreateUserAsync(User user, string password, ClaimsPrincipal adminUser)
    {

        if (adminUser == null || !adminUser.Identity.IsAuthenticated || !adminUser.IsInRole("Admin"))
        {
            return IdentityResult.Failed(new IdentityError { Description = "Only authenticated admins can create users." });
        }

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            return result;
        }

        return IdentityResult.Success;
    }

}