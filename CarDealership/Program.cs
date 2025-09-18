using CarDealership.Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc();
builder.Services.AddScoped<CarRepository>();

var cs = builder.Configuration.GetConnectionString("DealershipDbLocalConnection");
Console.WriteLine($"DEBUG: connection string = {cs}");

builder.Services.AddDbContext<DealershipDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DealershipDbLocalConnection")));


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

app.Run();
