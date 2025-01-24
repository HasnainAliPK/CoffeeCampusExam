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
                       CoffeeCampusDbContext context) //Kontruktor
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _context = context;
    }

    public async Task<bool> LoginAsync(string userName, string password) //Login Metode
    {
        var result = await _signInManager.PasswordSignInAsync(
            userName, // use username directly
             password,
             isPersistent: true, //"Husk mig" knap
             lockoutOnFailure: true); //Suspendere hvis koden skrives forkert flere gange

        return result.Succeeded;
    }

    public async Task LogoutAsync() //Metode til at logge ud
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<IdentityResult> RegisterAdminAsync(User admin, string password) //Metode for en admin bliver registeret
    {
        var result = await _userManager.CreateAsync(admin, password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(admin, "Admin");
        }
        return result;
    }


    public async Task<IdentityResult> CreateUserAsync(User user, string password, ClaimsPrincipal adminUser) //Metode til admin kan lave en user
    {

        if (adminUser == null || !adminUser.Identity.IsAuthenticated || !adminUser.IsInRole("Admin"))
        {
            return IdentityResult.Failed(new IdentityError { Description = "Only authenticated admins can create users." }); //Hvis brugeren ikke er en admin
        }
            var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            return result;
        }

        return IdentityResult.Success;
    }

}