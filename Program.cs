using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModelContextProtocol.Server;
using System.ComponentModel;

using Microsoft.Extensions.AI;
using ModelContextProtocol;

var builder = Host.CreateEmptyApplicationBuilder(settings: null);

builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly()
    .WithPromptsFromAssembly();

await builder.Build().RunAsync();

[McpServerToolType]
public static class EchoTool
{
    [McpServerTool, Description("Echoes the message back to the client.")]
    public static string Echo(string message) => $"Hello from C#: {message}";

    [McpServerTool, Description("Echoes in reverse the message sent.")]
    public static string ReverseEcho(string message) => new string(message.Reverse().ToArray());

    [McpServerTool, Description("Analyzes text using the client's LLM via elicitation (sampling).")]
    public static async Task<string> AnalyzeWithElicitation(
        McpServer thisServer,
        [Description("The text to analyze")] string text,
        CancellationToken cancellationToken = default)
    {
        // This demonstrates elicitation: calling back to the client's LLM
        var chatClient = thisServer.AsSamplingChatClient();
        
        var response = await chatClient.GetResponseAsync(
            new ChatMessage(ChatRole.User, $"Analyze this text and provide 2-3 key insights:\n\n{text}"),
            new ChatOptions { MaxOutputTokens = 256 },
            cancellationToken);

        return response?.Text ?? "No response from LLM";
    }
}

[McpServerResourceType]
public static class EchoResource
{
    [McpServerResource, Description("A simple text resource.")]
    public static string GetText() => "Hello from C#";
}

[McpServerPromptType]
public static class DemoPrompts
{
    [McpServerPrompt, Description("A prompt template for summarizing text content.")]
    public static ChatMessage SummarizePrompt([Description("The text content to summarize")] string content) =>
        new(ChatRole.User, $"""
        Please provide a concise summary of the following text in 2-3 sentences:

        {content}
        """);

    [McpServerPrompt, Description("A prompt template for analyzing sentiment of text.")]
    public static ChatMessage SentimentAnalysisPrompt([Description("The text to analyze")] string text) =>
        new(ChatRole.User, $"""
        Analyze the sentiment of the following text. Respond with only one word: positive, negative, or neutral.

        {text}
        """);

    [McpServerPrompt, Description("A prompt template for generating creative ideas.")]
    public static ChatMessage IdeaGeneratorPrompt([Description("The topic or theme")] string topic) =>
        new(ChatRole.User, $"""
        Generate 5 creative and innovative ideas related to: {topic}

        Format your response as a numbered list.
        """);
}
