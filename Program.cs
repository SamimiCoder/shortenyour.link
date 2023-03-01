using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Internal;
using shortenyour.link.Areas.Identity.Data;
using shortenyour.link.Data;
using shortenyour.link.Models;
using shortenyour.link.Services;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Core;
using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("MySqlIdentity") ?? throw new InvalidOperationException("Connection string 'MemberContextConnection' not found.");

builder.Services.AddDbContext<MemberContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(ServerVersion.AutoDetect(connectionString))), ServiceLifetime.Transient);
builder.Services.AddDbContext<ModsContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(ServerVersion.AutoDetect(connectionString))));
builder.Services.AddDbContext<LinkContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(ServerVersion.AutoDetect(connectionString))));
builder.Services.AddDefaultIdentity<shortenyourlinkUser>(options => options.SignIn.RequireConfirmedAccount = true)
.AddEntityFrameworkStores<MemberContext>();

// var optionsBuilder = new DbContextOptionsBuilder<ModsContext>();
// optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(ServerVersion.AutoDetect(connectionString)));
// var modsContext = new ModsContext(optionsBuilder.Options);
// ModService mods = new ModService(modsContext);
Logger log = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("Logs/log.txt")
    .WriteTo.MySQL(connectionString, "Logs", Serilog.Events.LogEventLevel.Information, false, 100, null)

    .CreateLogger();

builder.Host.UseSerilog(log);
builder.Services.AddTransient<IEmailSender, EmailSender>();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddMvc();

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.ResponseHeaders.Add("MyResponseHeader");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

var app = builder.Build();

// app.Use((context, next) =>
// {
//     if (mods.MaintenanceIsEnabled())
//     {
//         if (context.Request.Path != "/Home/Maintenance")
//         {
//             context.Request.Scheme = "http";
//             context.Response.Redirect("/Home/Maintenance");
//         }
//     }
//     else
//     {
//         next.Invoke();
//     }

//     return next();
// });

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseSerilogRequestLogging();
app.UseHttpLogging();
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