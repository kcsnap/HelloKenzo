using HelloKenzo.Web.Data;
using HelloKenzo.Web.Interfaces;
using HelloKenzo.Web.Models;

namespace HelloKenzo.Web.Services;

public class RegistrationService : IRegistrationService
{
    private readonly AppDbContext _dbContext;

    public RegistrationService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool Register(RegistrationRequest request)
    {
        Console.WriteLine($"Registration received: Name={request.Name}, Email={request.Email}");

        var isSuccessful = string.Equals(request.Name, "Kenzo", StringComparison.OrdinalIgnoreCase);

        var registration = new Registration
        {
            Name = request.Name,
            Email = request.Email,
            IsSuccessful = isSuccessful,
            CreatedAt = DateTime.UtcNow
        };

        _dbContext.Registrations.Add(registration);
        _dbContext.SaveChanges();

        return isSuccessful;
    }
}
