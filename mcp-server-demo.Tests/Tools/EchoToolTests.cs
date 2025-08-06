using FluentAssertions;

namespace mcp_server_demo.Tests.Tools;

public class EchoToolTests
{
    [Theory]
    [InlineData("Hello World", "Hello from C#: Hello World")]
    [InlineData("", "Hello from C#: ")]
    [InlineData("Test", "Hello from C#: Test")]
    [InlineData("Multiple Words Here", "Hello from C#: Multiple Words Here")]
    public void Echo_WithValidInput_ReturnsExpectedFormat(string input, string expected)
    {
        // Arrange - No setup needed for static method

        // Act
        var actual = EchoTool.Echo(input);

        // Assert
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData("Hello", "olleH")]
    [InlineData("World", "dlroW")]
    [InlineData("", "")]
    [InlineData("A", "A")]
    [InlineData("Test123", "321tseT")]
    [InlineData("Hello World", "dlroW olleH")]
    public void ReverseEcho_WithValidInput_ReturnsReversedString(string input, string expected)
    {
        // Arrange - No setup needed for static method

        // Act
        var actual = EchoTool.ReverseEcho(input);

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Echo_WithNullInput_HandlesGracefully()
    {
        // Arrange & Act
        var result = EchoTool.Echo(null!);

        // Assert
        result.Should().Be("Hello from C#: ");
    }

    [Fact]
    public void ReverseEcho_WithNullInput_ThrowsArgumentNullException()
    {
        // Arrange
        string? nullInput = null;

        // Act & Assert
        var act = () => EchoTool.ReverseEcho(nullInput!);
        act.Should().Throw<ArgumentNullException>();
    }
}