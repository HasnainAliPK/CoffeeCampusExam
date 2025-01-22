using CoffeeCampus.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CoffeeCampus.Data
{
    public class CoffeeCampusDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public CoffeeCampusDbContext(DbContextOptions<CoffeeCampusDbContext> options)
            : base(options) {
        }

        public DbSet<CoffeeMachine> CoffeeMachines { get; set; }
        public DbSet<Refill> Refills { get; set; }
        public DbSet<MachineCleaning> MachineCleanings { get; set; }
        public DbSet<HoseChange> HoseChanges { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<HoseChangeLog> HoseChangeLogs { get; set; }
        public DbSet<ServiceLog> ServiceLogs { get; set; }
        public DbSet<BinEmptying> BinEmptyings { get; set; }
        public DbSet<User> User { get; set; }


        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder); // Identity tabeller

            // Seed roller
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER" }
            );
        }
    }
}