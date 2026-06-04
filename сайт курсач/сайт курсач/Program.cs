using Microsoft.EntityFrameworkCore;
using сайт_курсач.Data;
using сайт_курсач.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSession();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();


// Создание роли Admin и пользователя admin

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider
        .GetRequiredService<ApplicationDbContext>();

    if (!context.Roles.Any())
    {
        context.Roles.Add(new Role
        {
            Name = "Admin"
        });

        context.SaveChanges();
    }

    if (!context.Users.Any())
    {
        var adminRole = context.Roles
            .First(r => r.Name == "Admin");

        context.Users.Add(new User
        {
            Login = "admin",
            Password = "admin123",
            Email = "admin@beauty.com",
            RoleId = adminRole.Id
        });

        context.SaveChanges();
    }
}


// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();