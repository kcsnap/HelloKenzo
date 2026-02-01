using HelloKenzo.Web.Data;
using HelloKenzo.Web.Models;
using HelloKenzo.Web.Services;
using Microsoft.EntityFrameworkCore;

namespace HelloKenzo.Tests;

public class RegistrationServiceTests
{
    private AppDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public void Register_WithNameKenzo_ReturnsTrue()
    {
        // Arrange
        using var dbContext = CreateDbContext();
        var sut = new RegistrationService(dbContext);
        var request = new RegistrationRequest("Kenzo", "kenzo@example.com");

        // Act
        var result = sut.Register(request);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Register_WithNameKenzoCaseInsensitive_ReturnsTrue()
    {
        // Arrange
        using var dbContext = CreateDbContext();
        var sut = new RegistrationService(dbContext);
        var request = new RegistrationRequest("KENZO", "kenzo@example.com");

        // Act
        var result = sut.Register(request);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Register_WithNameKenzoLowercase_ReturnsTrue()
    {
        // Arrange
        using var dbContext = CreateDbContext();
        var sut = new RegistrationService(dbContext);
        var request = new RegistrationRequest("kenzo", "kenzo@example.com");

        // Act
        var result = sut.Register(request);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Register_WithDifferentName_ReturnsFalse()
    {
        // Arrange
        using var dbContext = CreateDbContext();
        var sut = new RegistrationService(dbContext);
        var request = new RegistrationRequest("John", "john@example.com");

        // Act
        var result = sut.Register(request);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Register_WithEmptyName_ReturnsFalse()
    {
        // Arrange
        using var dbContext = CreateDbContext();
        var sut = new RegistrationService(dbContext);
        var request = new RegistrationRequest("", "test@example.com");

        // Act
        var result = sut.Register(request);

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData("Kenzo", true)]
    [InlineData("kenzo", true)]
    [InlineData("KENZO", true)]
    [InlineData("KeNzO", true)]
    [InlineData("John", false)]
    [InlineData("Jane", false)]
    [InlineData("", false)]
    [InlineData("Kenzo123", false)]
    public void Register_WithVariousNames_ReturnsExpectedResult(string name, bool expected)
    {
        // Arrange
        using var dbContext = CreateDbContext();
        var sut = new RegistrationService(dbContext);
        var request = new RegistrationRequest(name, "test@example.com");

        // Act
        var result = sut.Register(request);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Register_SavesRegistrationToDatabase()
    {
        // Arrange
        using var dbContext = CreateDbContext();
        var sut = new RegistrationService(dbContext);
        var request = new RegistrationRequest("Kenzo", "kenzo@example.com");

        // Act
        sut.Register(request);

        // Assert
        var registration = dbContext.Registrations.FirstOrDefault();
        Assert.NotNull(registration);
        Assert.Equal("Kenzo", registration.Name);
        Assert.Equal("kenzo@example.com", registration.Email);
        Assert.True(registration.IsSuccessful);
    }

    [Fact]
    public void Register_FailedRegistration_SavesWithIsSuccessfulFalse()
    {
        // Arrange
        using var dbContext = CreateDbContext();
        var sut = new RegistrationService(dbContext);
        var request = new RegistrationRequest("John", "john@example.com");

        // Act
        sut.Register(request);

        // Assert
        var registration = dbContext.Registrations.FirstOrDefault();
        Assert.NotNull(registration);
        Assert.Equal("John", registration.Name);
        Assert.Equal("john@example.com", registration.Email);
        Assert.False(registration.IsSuccessful);
    }
}
