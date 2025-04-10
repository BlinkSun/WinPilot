namespace WinPilot.Services;

/// <summary>
/// Represents a chat model supported by WinPilot, from any provider (OpenAI, Anthropic, etc.).
/// </summary>
public sealed class ChatModel
{
    /// <summary>
    /// Internal model ID used in API calls.
    /// </summary>
    public string ModelId { get; }

    /// <summary>
    /// Display name shown to the user.
    /// </summary>
    public string DisplayName { get; }

    /// <summary>
    /// Provider name, e.g. "OpenAI" or "Anthropic".
    /// </summary>
    public string Provider { get; }

    private ChatModel(string modelId, string displayName, string provider)
    {
        ModelId = modelId;
        DisplayName = displayName;
        Provider = provider;
    }

    // OPENAI MODELS
    public static readonly ChatModel Gpt4o = new("gpt-4o", "GPT-4o", "OpenAI");
    public static readonly ChatModel Gpt4oMini = new("gpt-4o-mini", "GPT-4o Mini", "OpenAI");
    public static readonly ChatModel Gpt45Preview = new("gpt-4.5-preview", "GPT-4.5 Preview", "OpenAI");
    public static readonly ChatModel Gpt4Turbo = new("gpt-4-turbo", "GPT-4 Turbo", "OpenAI");
    public static readonly ChatModel Gpt4Turbo_2024_04_09 = new("gpt-4-turbo-2024-04-09", "GPT-4 Turbo (2024-04-09)", "OpenAI");

    // ANTHROPIC MODELS
    public static readonly ChatModel Claude37Sonnet = new("claude-3-7-sonnet-20250219", "Claude 3.7 Sonnet", "Anthropic");
    public static readonly ChatModel Claude35Haiku = new("claude-3-5-haiku-20241022", "Claude 3.5 Haiku", "Anthropic");
    public static readonly ChatModel Claude35SonnetV2 = new("claude-3-5-sonnet-20241022", "Claude 3.5 Sonnet v2", "Anthropic");
    public static readonly ChatModel Claude35Sonnet = new("claude-3-5-sonnet-20240620", "Claude 3.5 Sonnet", "Anthropic");
    public static readonly ChatModel Claude3Opus = new("claude-3-opus-20240229", "Claude 3 Opus", "Anthropic");
    public static readonly ChatModel Claude3Sonnet = new("claude-3-sonnet-20240229", "Claude 3 Sonnet", "Anthropic");
    public static readonly ChatModel Claude3Haiku = new("claude-3-haiku-20240307", "Claude 3 Haiku", "Anthropic");

    /// <summary>
    /// List of all available models.
    /// </summary>
    public static IReadOnlyList<ChatModel> AllModels { get; } =
    [
        // OpenAI
        Gpt4o,
        Gpt4oMini,
        Gpt45Preview,
        Gpt4Turbo,
        Gpt4Turbo_2024_04_09,

        // Anthropic
        Claude37Sonnet,
        Claude35Haiku,
        Claude35SonnetV2,
        Claude35Sonnet,
        Claude3Opus,
        Claude3Sonnet,
        Claude3Haiku
    ];

    public override string ToString()
    {
        return DisplayName;
    }
}