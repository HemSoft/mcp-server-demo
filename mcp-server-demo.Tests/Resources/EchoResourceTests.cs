using FluentAssertions;

namespace mcp_server_demo.Tests.Resources;

public class EchoResourceTests
{
    [Fact]
    public void GetText_WhenCalled_ReturnsExpectedMessage()
    {
        // Arrange
        const string expected = "Hello from C#";

        // Act
        var actual = EchoResource.GetText();

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GetText_CalledMultipleTimes_ReturnsConsistentResult()
    {
        // Arrange & Act
        var firstCall = EchoResource.GetText();
        var secondCall = EchoResource.GetText();
        var thirdCall = EchoResource.GetText();

        // Assert
        firstCall.Should().Be(secondCall);
        secondCall.Should().Be(thirdCall);
        firstCall.Should().Be("Hello from C#");
    }

    [Fact]
    public void GetText_WhenCalled_ReturnsNonEmptyString()
    {
        // Act
        var result = EchoResource.GetText();

        // Assert
        result.Should().NotBeNullOrEmpty();
        result.Should().NotBeNullOrWhiteSpace();
    }
}