using CoffeeCampus.Data;
using CoffeeCampus.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CoffeeCampus.Services
{
    public class MockEmailStore : UserStore<User, IdentityRole, CoffeeCampusDbContext, string>, IUserEmailStore<User>
    {
        public MockEmailStore(CoffeeCampusDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {

        }

        public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.CompletedTask;
        }

        public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(true); //Always return true to bypass confirmation
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = true; // Set the email confirmed to true.
            return Task.CompletedTask;
        }

        public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }

}