using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Internal;
using shortenyour.link.Areas.Identity.Data;
using shortenyour.link.Data;
using shortenyour.link.Models;
using shortenyour.link.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("MySqlIdentity") ?? throw new InvalidOperationException("Connection string 'MemberContextConnection' not found.");

builder.Services.AddDbContext<MemberContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(ServerVersion.AutoDetect(connectionString))));
builder.Services.AddDbContext<AnnouncementContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(ServerVersion.AutoDetect(connectionString))));
builder.Services.AddDbContext<LinkContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(ServerVersion.AutoDetect(connectionString))));
builder.Services.AddDbContext<AdminContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(ServerVersion.AutoDetect(connectionString))));
builder.Services.AddDefaultIdentity<shortenyourlinkUser>(options => options.SignIn.RequireConfirmedAccount = true)
.AddEntityFrameworkStores<MemberContext>();

builder.Services.AddTransient<IEmailSender, EmailSender>();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddMvc();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
        name: "shortUrls",
        pattern: "{shortUrl}",
        defaults: new { controller = "Home", action = "Redirect" });
app.MapRazorPages();
app.Run();