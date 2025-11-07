using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// add MVC/Razor
builder.Services.AddControllersWithViews();

// HttpClient pointing to API - name "NorthWindAPI"
builder.Services.AddHttpClient("NorthWindAPI", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["NorthWindAPI:BaseUrl"] ?? "https://localhost:5001");
});

// (OPTIONAL) Authentication setup here - cookie/OIDC etc.
// For quick testing you can skip complex auth and call API endpoints that are not [Authorize].

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
