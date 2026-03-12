using HangMan.Data;
using HangMan.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Configuration.AddUserSecrets<Program>();

var conDatabase = builder.Configuration.GetConnectionString("MySqlConnection");
var user = builder.Configuration["DbUser"] ?? "unknownUser";
var password = builder.Configuration["DbPassword"];

var finalConn = $"{conDatabase}User={user};Password={password};";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(finalConn));

builder.Services.AddDefaultIdentity<PlayerModel>(options =>
    options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        try
        {
            SeedData s = new();
            s.Seed(appDbContext);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Seeding skipped: " + ex.Message);
        }
    }
}

app.Run();
