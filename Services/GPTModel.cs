namespace WinPilot.Services;

/// <summary>
/// Represents available OpenAI chat models that support multimodal input (text and images).
/// </summary>
public sealed class GPTModel
{
    /// <summary>
    /// Internal identifier used for API calls.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Friendly display name.
    /// </summary>
    public string DisplayName { get; }

    /// <summary>
    /// Constructor for creating a new instance of GPTModel.
    /// </summary>
    private GPTModel(string id, string displayName)
    {
        Id = id;
        DisplayName = displayName;
    }

    /// <summary>
    /// GPT-4o (latest, fastest multimodal model).
    /// </summary>
    public static readonly GPTModel Gpt4o = new("gpt-4o", "GPT-4o");

    /// <summary>
    /// GPT-4o Mini (lightweight multimodal model).
    /// </summary>
    public static readonly GPTModel Gpt4oMini = new("gpt-4o-mini", "GPT-4o Mini");

    /// <summary>
    /// GPT-4.5 Preview (new advanced multimodal model).
    /// </summary>
    public static readonly GPTModel Gpt45Preview = new("gpt-4.5-preview", "GPT-4.5 Preview");

    /// <summary>
    /// GPT-4 Turbo, optimized for speed and multimodal interactions.
    /// </summary>
    public static readonly GPTModel Gpt4Turbo = new("gpt-4-turbo", "GPT-4 Turbo");

    /// <summary>
    /// GPT-4 Turbo (specific version released on 2024-04-09).
    /// </summary>
    public static readonly GPTModel Gpt4Turbo_2024_04_09 = new("gpt-4-turbo-2024-04-09", "GPT-4 Turbo (2024-04-09)");

    /// <summary>
    /// Returns all available multimodal chat models.
    /// </summary>
    public static IReadOnlyList<GPTModel> AllModels { get; } =
    [
        Gpt4o,
        Gpt4oMini,
        Gpt45Preview,
        Gpt4Turbo,
        Gpt4Turbo_2024_04_09
    ];

    /// <summary>
    /// Overrides the ToString method to return the display name.
    /// </summary>
    public override string ToString()
    {
        return DisplayName;
    }
}