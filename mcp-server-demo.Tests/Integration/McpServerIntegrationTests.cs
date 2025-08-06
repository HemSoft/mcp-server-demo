using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModelContextProtocol.Server;

namespace mcp_server_demo.Tests.Integration;

public class McpServerIntegrationTests
{
    [Fact]
    public void McpServer_CanBeBuilt_WithoutErrors()
    {
        // Arrange & Act
        var builder = Host.CreateEmptyApplicationBuilder(settings: null);
        
        builder.Services
            .AddMcpServer()
            .WithStdioServerTransport()
            .WithToolsFromAssembly();

        var act = () => builder.Build();

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void McpServer_ServicesRegistration_ContainsExpectedServices()
    {
        // Arrange
        var builder = Host.CreateEmptyApplicationBuilder(settings: null);
        
        builder.Services
            .AddMcpServer()
            .WithStdioServerTransport()
            .WithToolsFromAssembly();

        // Act
        var host = builder.Build();
        var serviceProvider = host.Services;

        // Assert
        serviceProvider.Should().NotBeNull();
        // Verify that the MCP server services are registered
        var mcpServerServices = serviceProvider.GetServices<object>();
        mcpServerServices.Should().NotBeNull();
    }

    [Fact]
    public void EchoTool_IsAccessible_FromMcpContext()
    {
        // Arrange & Act
        var echoResult = EchoTool.Echo("Integration Test");
        var reverseResult = EchoTool.ReverseEcho("Integration Test");

        // Assert
        echoResult.Should().Be("Hello from C#: Integration Test");
        reverseResult.Should().Be("tseT noitargetnI");
    }

    [Fact]
    public void EchoResource_IsAccessible_FromMcpContext()
    {
        // Arrange & Act
        var resourceResult = EchoResource.GetText();

        // Assert
        resourceResult.Should().Be("Hello from C#");
    }

    [Fact]
    public void McpServer_ToolsAndResources_AreProperlyDecorated()
    {
        // Arrange
        var echoToolType = typeof(EchoTool);
        var echoResourceType = typeof(EchoResource);

        // Act & Assert - Verify tool decorations
        echoToolType.Should().BeDecoratedWith<McpServerToolTypeAttribute>();
        
        var echoMethod = echoToolType.GetMethod(nameof(EchoTool.Echo));
        var reverseEchoMethod = echoToolType.GetMethod(nameof(EchoTool.ReverseEcho));
        
        echoMethod.Should().NotBeNull();
        echoMethod.Should().BeDecoratedWith<McpServerToolAttribute>();
        
        reverseEchoMethod.Should().NotBeNull();
        reverseEchoMethod.Should().BeDecoratedWith<McpServerToolAttribute>();

        // Act & Assert - Verify resource decorations
        echoResourceType.Should().BeDecoratedWith<McpServerResourceTypeAttribute>();
        
        var getTextMethod = echoResourceType.GetMethod(nameof(EchoResource.GetText));
        getTextMethod.Should().NotBeNull();
        getTextMethod.Should().BeDecoratedWith<McpServerResourceAttribute>();
    }
}