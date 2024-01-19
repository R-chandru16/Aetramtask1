using Microsoft.EntityFrameworkCore;

using Aetramwork.Models;


var builder = WebApplication.CreateBuilder(args);


string connString = builder.Configuration.GetConnectionString("MyConStr");
builder.Services.AddDbContext<AppDbContext>(item => item.UseSqlite(connString));
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=DatabaseController}/{action=Index}/{id?}");
app.Run();