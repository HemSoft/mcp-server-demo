# MCP Server Demo

A sample Model Context Protocol (MCP) server built with C# and .NET that demonstrates how to create custom tools for AI assistants. This server provides two simple tools: Echo and ReverseEcho.

## Features

- **Echo Tool**: Returns a greeting message with the input text
- **ReverseEcho Tool**: Returns the input text with characters reversed
- Built with the ModelContextProtocol.Server library
- Compatible with any MCP host (VS Code, Claude Desktop, etc.)

## Prerequisites

- .NET 10.0 or later
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
         "cwd": "f:\\github\\HemSoft\\mcp-server-demo"
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
         "args": ["path/to/mcp-server-demo/bin/Release/net10.0/mcp-server-demo.dll"],
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

## Testing the Server

Once connected to an MCP host, you can test the tools by asking the AI assistant to:

- "Echo the message 'Hello MCP'"
- "Reverse the string 'Once upon a time'"
- "Use the echo tool to say something"

## Troubleshooting

### Server Not Connecting
- Ensure .NET 10.0 is installed and accessible via the `dotnet` command
- Check that the file paths in your configuration are correct
- Verify the server builds without errors (`dotnet build`)

### Tools Not Available
- Restart your MCP host application
- Check the MCP host logs for connection errors
- Ensure the configuration JSON is valid

## Development

This project demonstrates the basic structure of an MCP server in C#:

- `Program.cs`: Main entry point and tool definitions
- `mcp-server-demo.csproj`: Project file with dependencies
- Uses `ModelContextProtocol.Server` NuGet package

To add new tools:
1. Create a new static method in the `EchoTool` class (or create a new class)
2. Decorate with `[McpServerTool]` and `[Description]` attributes
3. Rebuild and restart the server

## License

This project is licensed under the MIT License - see the LICENSE file for details.
