using IdentityDemo.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<AccountService>();
var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();