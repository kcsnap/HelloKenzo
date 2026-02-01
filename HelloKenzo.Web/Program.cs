var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IRegistrationService, RegistrationService>();
var app = builder.Build();

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

app.Run();

public record RegistrationRequest(string Name, string Email);

public interface IRegistrationService
{
    bool Register(RegistrationRequest request);
}

public class RegistrationService : IRegistrationService
{
    public bool Register(RegistrationRequest request)
    {
        Console.WriteLine($"Registration received: Name={request.Name}, Email={request.Email}");

        return string.Equals(request.Name, "Kenzo", StringComparison.OrdinalIgnoreCase);
    }
}
