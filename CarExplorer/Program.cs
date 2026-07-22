using CarExplorer.Extensions;
using CarExplorer.Middleware;
using CarExplorer.Models.Settings;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

builder.Services.Configure<VehicleApiSettings>(
    builder.Configuration.GetSection("VehicleApi")
);
builder.Services.Configure<CacheSettings>(
    builder.Configuration.GetSection("CacheSettings")
);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMemoryCache();
builder.Services.AddApplicationServices();

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=CarExplorer}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
