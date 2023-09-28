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


var uset = new UserSet();
var pset = new PostSet();
User? user = uset.GetUser("reinir2");
Post p = pset.NewPost(user);
var pinfo = pset.GetPostInfo(p);
pinfo.Content = "Good evening, even tho its 8am rn...";
pset.UpdatePostInfo(pinfo);


app.Run();


