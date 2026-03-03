using HangMan.Data;
using HangMan.Data;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var conServer = builder.Configuration.GetConnectionString("MySqlConnectionServer");
var user = builder.Configuration["DbUser"];
var password = builder.Configuration["DbPassword"];

var finalConn = $"{conServer}User={user};Password={password};database=HangManDB";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(finalConn, ServerVersion.AutoDetect(finalConn)));

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

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

using (var scope = app.Services.CreateScope())
{
    var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    SeedData s = new();
    s.Seed(appDbContext);
}

app.Run();
