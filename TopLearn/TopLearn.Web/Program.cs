using AccountManagement.Infra.Configuration;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using _0.Framework.Application.Email;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
//==================================================================
services.AddControllersWithViews();
services.AddHttpContextAccessor();
services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.LoginPath = "/Login";
        options.LogoutPath = "/Logout";
        options.ExpireTimeSpan = TimeSpan.FromDays(10);
    });
//------------------------------------------------------------------

var connectionString = builder.Configuration.GetConnectionString("TopLearnConnection");

AccountManagementIoc.Configure(services, connectionString);

services.AddTransient<IViewRenderService, RenderViewToString>();
services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
