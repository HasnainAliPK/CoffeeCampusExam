using CoffeeCampus.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CoffeeCampus.Models;
using CoffeeCampus.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Configure DbContext
builder.Services.AddDbContext<CoffeeCampusDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
});

// Configure Identity services
// Configure Identity
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
  .AddEntityFrameworkStores<CoffeeCampusDbContext>()
     .AddDefaultTokenProviders() // Add Default Token providers
     .AddUserStore<MockEmailStore>(); //Replace the default UserStore for Admin users.
builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddRazorPages();

//Add services
builder.Services.AddScoped<AuthService>();



var app = builder.Build();




// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();