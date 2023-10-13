using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VeloceWebApp_7._0_.Data;
using VeloceWebApp_7._0_.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();

builder.Services.AddDbContext<VeloceWebApp_7_0_Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("VeloceWebApp_7_0_Context") ?? throw new InvalidOperationException("Connection string 'VeloceWebApp_7_0_Context' not found.")));
builder.Services.AddScoped<IPersonService,PersonService>();
// Add services to the container.
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
    pattern: "{controller=PersonModels}/{action=Index}/{id?}");

app.Run();
