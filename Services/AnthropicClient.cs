using System.Net.Http;
using System.Text;
using System.Text.Json;
using WinPilot.Interfaces;
using WinPilot.Managers;

namespace WinPilot.Services;

/// <summary>
/// A client for interacting with the Anthropic API.
/// </summary>
public class AnthropicClient : IAIProviderService
{
    private readonly HttpClient httpClient;

    /// <summary>
    /// Initializes a new instance of the AnthropicClient class with the API key from settings.
    /// </summary>
    public AnthropicClient() : this(SettingsManager.APIKey ?? string.Empty) { }

    /// <summary>
    /// Initializes a new instance of the AnthropicClient class with the provided API key.
    /// </summary>
    public AnthropicClient(string apiKey)
    {
        httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);
        httpClient.DefaultRequestHeaders.Add("anthropic-version", "2023-06-01");
    }

    /// <summary>
    /// Sends a request to the Anthropic API with the provided system and user prompts, and an optional screenshot.
    /// </summary>
    public async Task<string?> GetSuggestionAsync(string systemPrompt, string userPrompt, byte[]? screenshot)
    {

        if (string.IsNullOrWhiteSpace(systemPrompt))
            return null;

        string base64Image = screenshot != null
            ? Convert.ToBase64String(screenshot)
            : "";

        var payload = new
        {
            model = SettingsManager.SelectedModel,
            max_tokens = 1024,
            temperature = 0.7,
            system = systemPrompt,
            messages = new[]
            {
                new
                {
                    role = "user",
                    content = new object[]
                    {
                        new
                        {
                            type = "image",
                            source = new {
                                type = "base64",
                                media_type = "image/png",
                                data = $"{base64Image}"
                            }
                        },
                        new
                        {
                            type = "text",
                            text = userPrompt
                        }
                    }
                }
            }
        };

        string json = JsonSerializer.Serialize(payload);
        StringContent content = new(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await httpClient.PostAsync("https://api.anthropic.com/v1/messages", content);
        string responseJson = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            return $"Anthropic API error: {response.StatusCode}\n{responseJson}";

        using JsonDocument doc = JsonDocument.Parse(responseJson);
        JsonElement root = doc.RootElement;

        string? result = doc.RootElement
            .GetProperty("content")[0]
            .GetProperty("text")
            .GetString();

        httpClient.Dispose();

        return result;
    }
}
