using forum.Database;
using forum.Models;
using Microsoft.AspNetCore.Routing.Patterns;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();
builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(500);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();
//app.UseRouting();
app.UseSession();
app.MapControllers();
app.UseStaticFiles();
app.Run();


