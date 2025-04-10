namespace WinPilot.Interfaces;

public interface IAIProviderService
{
    Task<string?> GetSuggestionAsync(string systemPrompt, string userPrompt, byte[]? screenshotBytes);
}