namespace mcp_server_demo.Tests.TestUtilities;

public static class TestConstants
{
    public const string ExpectedEchoPrefix = "Hello from C#: ";
    public const string ExpectedResourceText = "Hello from C#";
    
    public static class TestMessages
    {
        public const string HelloWorld = "Hello World";
        public const string EmptyString = "";
        public const string SingleCharacter = "A";
        public const string WithNumbers = "Test123";
        public const string MultipleWords = "Multiple Words Here";
    }
    
    public static class ExpectedResults
    {
        public const string HelloWorldEcho = "Hello from C#: Hello World";
        public const string EmptyStringEcho = "Hello from C#: ";
        public const string SingleCharacterEcho = "Hello from C#: A";
        public const string WithNumbersEcho = "Hello from C#: Test123";
        public const string MultipleWordsEcho = "Hello from C#: Multiple Words Here";
        
        public const string HelloWorldReverse = "dlroW olleH";
        public const string EmptyStringReverse = "";
        public const string SingleCharacterReverse = "A";
        public const string WithNumbersReverse = "321tseT";
    }
}