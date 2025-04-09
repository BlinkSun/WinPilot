using System.Globalization;

namespace WinPilot.Managers;

/// <summary>
/// Manages the application's language settings.
/// </summary>
public static class LanguageManager
{
    private static readonly Dictionary<string, string> supportedLanguages = new()
    {
        { "en-US", "English" },
        { "es-ES", "Español" },
        { "de-DE", "Deutsch" },
        { "fr-CA", "Français" }
    };

    /// <summary>
    /// Gets the supported languages for the application.
    /// </summary>
    public static IReadOnlyDictionary<string, string> SupportedLanguages => supportedLanguages;

    /// <summary>
    /// Gets the current language code of the application.
    /// </summary>
    public static string CurrentLanguageCode => Thread.CurrentThread.CurrentUICulture.Name;

    /// <summary>
    /// Applies the specified language to the application.
    /// </summary>
    public static void ApplyLanguage(string languageCode)
    {
        if (!supportedLanguages.ContainsKey(languageCode))
            throw new ArgumentException($"Unsupported language: {languageCode}");

        CultureInfo culture = new(languageCode);

        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;

        SettingsManager.Language = languageCode;
        SettingsManager.Save();
    }

    /// <summary>
    /// Initializes the application language based on user settings.
    /// </summary>
    public static void InitializeFromSettings()
    {
        string lang = SettingsManager.Language;

        if (!string.IsNullOrWhiteSpace(lang) && supportedLanguages.ContainsKey(lang))
        {
            ApplyLanguage(lang);
        }
    }
}