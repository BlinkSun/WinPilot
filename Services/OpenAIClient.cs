using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using WinPilot.Managers;

namespace WinPilot.Services;

/// <summary>
/// A client for interacting with the OpenAI API.
/// </summary>
public class OpenAIClient
{
    private readonly HttpClient httpClient;

    /// <summary>
    /// Initializes a new instance of the OpenAIClient class with the API key from settings.
    /// </summary>
    public OpenAIClient() : this(SettingsManager.OpenAIKey ?? string.Empty) { }

    /// <summary>
    /// Initializes a new instance of the OpenAIClient class with the provided API key.
    /// </summary>
    public OpenAIClient(string apiKey)
    {
        httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
    }

    /// <summary>
    /// Sends a request to the OpenAI API with the provided system and user prompts, and an optional screenshot.
    /// </summary>
    public async Task<string?> SendContextToGPTAsync(string systemPrompt, string userPrompt, byte[]? screenshot)
    {
        if (string.IsNullOrWhiteSpace(systemPrompt))
            return null;

        string base64Image = screenshot != null
            ? Convert.ToBase64String(screenshot)
            : "";

        var payload = new
        {
            model = SettingsManager.SelectedModel,
            messages = new object[]
            {
                new {
                    role = "system",
                    content = systemPrompt
                },
                new {
                    role = "user",
                    content = new object[]
                    {
                        new {
                            type = "text",
                            text = userPrompt
                        },
                        new {
                            type = "image_url",
                            image_url = new {
                                url = $"data:image/png;base64,{base64Image}"
                            }
                        }
                    }
                }
            },
            max_tokens = 1000
        };

        string json = JsonSerializer.Serialize(payload);
        StringContent content = new(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
        string responseJson = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            return $"OpenAI API error: {response.StatusCode}\n{responseJson}";

        using JsonDocument doc = JsonDocument.Parse(responseJson);
        string? result = doc.RootElement
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString();

        httpClient.Dispose();

        return result;
    }
}