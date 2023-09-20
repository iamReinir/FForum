var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();
builder.Services.AddRazorPages();
var app = builder.Build();
//app.UseRouting();
app.UseHttpsRedirection();
app.MapControllers();
app.UseStaticFiles();
app.Run();