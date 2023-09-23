using forum.Database;
using forum.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();
builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(100);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();
//app.UseRouting();
app.UseSession();
app.MapControllers();
app.UseStaticFiles();
app.Run();

/*
var uset = new UserSet();
uset.Register("reinir3", "123456",);
Console.WriteLine(uset.UserInfos.ElementAt(0).Name);
*/