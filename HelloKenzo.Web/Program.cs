using HelloKenzo.Web.Data;
using HelloKenzo.Web.Interfaces;
using HelloKenzo.Web.Models;
using HelloKenzo.Web.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=HelloKenzo.db"));

builder.Services.AddScoped<IRegistrationService, RegistrationService>();

var app = builder.Build();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapPost("/api/register", (RegistrationRequest request, IRegistrationService registrationService) =>
{
    var result = registrationService.Register(request);

    if (result)
    {
        return Results.Ok(new { success = true, message = "Registration successful" });
    }

    return Results.BadRequest(new { success = false, message = "Registration failed. Name must be 'Kenzo'." });
});

app.MapGet("/api/registrations", (AppDbContext db) =>
{
    return Results.Ok(db.Registrations.ToList());
});

app.Run();
