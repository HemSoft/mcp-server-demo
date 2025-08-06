# MCP Server Demo

A sample Model Context Protocol (MCP) server built with C# and .NET that demonstrates how to create custom tools and resources for AI assistants. This server provides two simple tools (Echo and ReverseEcho) and one resource (Text).

## Features

- **Echo Tool**: Returns a greeting message with the input text
- **ReverseEcho Tool**: Returns the input text with characters reversed
- **Text Resource**: Provides a simple text resource for AI assistants
- Built with the ModelContextProtocol.Server library
- Compatible with any MCP host (VS Code, Claude Desktop, etc.)

## Prerequisites

- .NET 8.0 or later
- VS Code with GitHub Copilot extension (for VS Code integration)

## Building the Server

1. Clone this repository:
   ```bash
   git clone https://github.com/HemSoft/mcp-server-demo.git
   cd mcp-server-demo
   ```

2. Build the project:
   ```bash
   dotnet build
   ```

3. Run the server (for testing):
   ```bash
   dotnet run
   ```

## Using with VS Code

To use this MCP server with VS Code and GitHub Copilot:

### Method 1: Using the VS Code MCP Extension

1. Install the MCP extension in VS Code
2. Open VS Code settings and navigate to the MCP configuration
3. Add a new server configuration:
   ```json
   {
     "mcpServers": {
       "mcp-server-demo": {
         "command": "dotnet",
         "args": ["run", "--project", "path/to/mcp-server-demo/mcp-server-demo.csproj"],
         "env": {}
       }
     }
   }
   ```

### Method 2: Using MCP Configuration File

1. Create or edit the MCP configuration file in your VS Code workspace (`.vscode/mcp.json`):
   ```json
   {
     "mcpServers": {
       "mcp-server-demo": {
         "command": "dotnet",
         "args": ["run", "--project", "./mcp-server-demo.csproj"],
         "cwd": "/path/to/your/mcp-server-demo"
       }
     }
   }
   ```

2. Restart VS Code or reload the window
3. The MCP server will be automatically connected when GitHub Copilot starts

## Using with Claude Desktop

To use this server with Claude Desktop:

1. Build the project in Release mode:
   ```bash
   dotnet build -c Release
   ```

2. Edit your Claude Desktop configuration file:
   - **Windows**: `%APPDATA%\Claude\claude_desktop_config.json`
   - **macOS**: `~/Library/Application Support/Claude/claude_desktop_config.json`
   - **Linux**: `~/.config/claude/claude_desktop_config.json`

3. Add the server configuration:
   ```json
   {
     "mcpServers": {
       "mcp-server-demo": {
         "command": "dotnet",
         "args": ["path/to/mcp-server-demo/bin/Release/net8.0/mcp-server-demo.dll"],
         "env": {}
       }
     }
   }
   ```

4. Restart Claude Desktop

## Available Tools

### Echo
- **Description**: Echoes the message back to the client with a greeting
- **Parameters**:
  - `message` (string): The message to echo
- **Example**: Input "Hello World" returns "Hello from C#: Hello World"

### ReverseEcho
- **Description**: Returns the input message with characters in reverse order
- **Parameters**:
  - `message` (string): The message to reverse
- **Example**: Input "Hello World" returns "dlroW olleH"

## Available Resources

### Text Resource
- **Description**: Provides a simple text resource that returns "Hello from C#"
- **URI**: Can be accessed through MCP resource calls
- **Usage**: AI assistants can retrieve this resource to get the static text content

## Testing the Server

Once connected to an MCP host, you can test the tools by asking the AI assistant to:

- "Echo the message 'Hello MCP'"
- "Reverse the string 'Once upon a time'"
- "Use the echo tool to say something"

You can also test the resource by asking the AI assistant to:

- "Access the text resource from the MCP server"
- "Retrieve the available text content"

## Troubleshooting

### Server Not Connecting
- Ensure .NET 8.0 is installed and accessible via the `dotnet` command
- Check that the file paths in your configuration are correct
- Verify the server builds without errors (`dotnet build`)

### Tools Not Available
- Restart your MCP host application
- Check the MCP host logs for connection errors
- Ensure the configuration JSON is valid

## Development

This project demonstrates the basic structure of an MCP server in C#:

- `Program.cs`: Main entry point with tool and resource definitions
- `mcp-server-demo.csproj`: Project file with dependencies
- Uses `ModelContextProtocol.Server` NuGet package

To add new tools:
1. Create a new static method in the `EchoTool` class (or create a new class)
2. Decorate with `[McpServerTool]` and `[Description]` attributes
3. Rebuild and restart the server

To add new resources:
1. Create a new static method in the `EchoResource` class (or create a new class)
2. Decorate with `[McpServerResource]` and `[Description]` attributes
3. Rebuild and restart the server

## Testing

This project includes comprehensive testing infrastructure to ensure code quality and provide examples of MCP server testing patterns.

### Running Tests

```bash
# Run all tests
dotnet test

# Run tests with detailed output
dotnet test --verbosity normal

# Run tests with code coverage
dotnet test --collect:"XPlat Code Coverage"
```

### Test Structure

The test project follows best practices with organized test categories:

- **Unit Tests**: Test individual components in isolation
  - `Tools/EchoToolTests.cs`: Tests for Echo and ReverseEcho tools
  - `Resources/EchoResourceTests.cs`: Tests for text resource functionality
- **Integration Tests**: Test MCP server functionality end-to-end
  - `Integration/McpServerIntegrationTests.cs`: Server startup and component integration
- **Test Utilities**: Shared constants and helper methods
  - `TestUtilities/TestConstants.cs`: Common test data and expected results

### Testing Best Practices Demonstrated

- ✅ **Clear naming**: `Method_Scenario_ExpectedResult` pattern
- ✅ **Arrange-Act-Assert** pattern in all tests
- ✅ **Parameterized tests** using `[Theory]` and `[InlineData]`
- ✅ **No magic strings** - constants defined in `TestConstants`
- ✅ **Comprehensive coverage** of edge cases (null inputs, empty strings)
- ✅ **Integration testing** for MCP server components
- ✅ **Fluent assertions** for readable test assertions

### Adding New Tests

When adding new MCP tools or resources:

1. **Create unit tests** in the appropriate folder (`Tools/` or `Resources/`)
2. **Test edge cases** including null inputs, empty strings, and boundary conditions
3. **Add integration tests** if the component interacts with the MCP server
4. **Update test constants** if you introduce new test data
5. **Follow naming conventions** and use Arrange-Act-Assert pattern

Example test structure:
```csharp
[Theory]
[InlineData("input", "expectedOutput")]
[InlineData("", "expectedForEmpty")]
public void MethodName_WithScenario_ReturnsExpectedResult(string input, string expected)
{
    // Arrange - Set up test data

    // Act - Execute the method under test
    var actual = MethodUnderTest(input);

    // Assert - Verify the result
    actual.Should().Be(expected);
}
```

## Continuous Integration

This project uses GitHub Actions for automated testing and builds:

- **Automated Testing**: All tests run on every push and pull request
- **Multi-Platform Builds**: Builds and publishes artifacts for Linux and Windows
- **Code Coverage**: Tracks test coverage and reports to Codecov
- **Quality Gates**: Pull requests must pass all tests before merging

The CI/CD pipeline ensures code quality and prevents regressions by:
1. Running all unit and integration tests
2. Building the project in Release configuration
3. Publishing platform-specific artifacts
4. Collecting and reporting code coverage metrics

## License

This project is licensed under the MIT License - see the LICENSE file for details.
