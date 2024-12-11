using IdentityDemo;
using IdentityDemo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(connectionString));

builder.Services.AddControllersWithViews();
builder.Services.AddTransient<AccountService>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Här kan vi (om vi vill) ange inställningar för t.ex. lösenord
    // (ofta struntar man i detta och kör på default-värdena)
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();


builder.Services.ConfigureApplicationCookie(o => o.LoginPath = "/login");
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();