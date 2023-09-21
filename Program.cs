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

var uset = new UserSet();
var uinfo = new UserInfo("Reinir");
if(uset.GetUser("reinir1") != null)
{
    Console.WriteLine("Got him");
}

app.UseSession();
app.MapControllers();
app.UseStaticFiles();
app.Run();