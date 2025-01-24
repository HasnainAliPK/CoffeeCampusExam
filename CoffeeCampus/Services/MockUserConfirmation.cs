using CoffeeCampus.Data;
using CoffeeCampus.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CoffeeCampus.Services
{
    public class MockEmailStore : UserStore<User, IdentityRole, CoffeeCampusDbContext, string>, IUserEmailStore<User> //Klassen MockEmailstore arver fra forskellige klasser og en interface
    {
        public MockEmailStore(CoffeeCampusDbContext context, IdentityErrorDescriber describer = null) : base(context, describer) //Konstruktor
        {

        }

        public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken) //Metode til opdatere e-mail
        {
            user.Email = email;
            return Task.CompletedTask;
        }

        public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken) //Metode til at hente email
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken) //Metode til godkende E-mail
        {
            return Task.FromResult(true); 
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken) //Metode til at E-mail er godkendt
        {
            user.EmailConfirmed = true; 
            return Task.CompletedTask;
        }

        public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken) //Metode til få sin opdateret mail 
        {
            return Task.FromResult(user.Email);
        }

        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken) //Metode til at normalisere den opdateret mail
        {
            return Task.CompletedTask;
        }
    }

}