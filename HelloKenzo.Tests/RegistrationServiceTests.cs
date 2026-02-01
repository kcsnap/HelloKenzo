namespace HelloKenzo.Tests;

public class RegistrationServiceTests
{
    private readonly RegistrationService _sut;

    public RegistrationServiceTests()
    {
        _sut = new RegistrationService();
    }

    [Fact]
    public void Register_WithNameKenzo_ReturnsTrue()
    {
        // Arrange
        var request = new RegistrationRequest("Kenzo", "kenzo@example.com");

        // Act
        var result = _sut.Register(request);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Register_WithNameKenzoCaseInsensitive_ReturnsTrue()
    {
        // Arrange
        var request = new RegistrationRequest("KENZO", "kenzo@example.com");

        // Act
        var result = _sut.Register(request);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Register_WithNameKenzoLowercase_ReturnsTrue()
    {
        // Arrange
        var request = new RegistrationRequest("kenzo", "kenzo@example.com");

        // Act
        var result = _sut.Register(request);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Register_WithDifferentName_ReturnsFalse()
    {
        // Arrange
        var request = new RegistrationRequest("John", "john@example.com");

        // Act
        var result = _sut.Register(request);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Register_WithEmptyName_ReturnsFalse()
    {
        // Arrange
        var request = new RegistrationRequest("", "test@example.com");

        // Act
        var result = _sut.Register(request);

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
        var request = new RegistrationRequest(name, "test@example.com");

        // Act
        var result = _sut.Register(request);

        // Assert
        Assert.Equal(expected, result);
    }
}
