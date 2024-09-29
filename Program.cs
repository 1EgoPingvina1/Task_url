using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Extensions;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<UrlDataContext>();
    await context.Database.MigrateAsync(); 
    
    //Строка обновляет Seeding данных в базе
    // await context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE Urls");
    await Seed.SeedData(context);
}
catch (Exception e)
{
    var loger = services.GetService<ILogger<Program>>();
    loger.LogError(builder.Configuration.GetConnectionString("DefaultConnection"));
    loger.LogError(e, "Invalid migration, please check your connection string.");
}
app.Run();