using System.Windows.Input;

namespace WinPilot.Managers;

/// <summary>
/// SettingsManager is a singleton class that manages application settings.
/// </summary>
public class SettingsManager
{
    private static readonly Properties.Settings settings = Properties.Settings.Default;

    /// <summary>
    /// Gets or sets the language setting for the application.
    /// </summary>
    public static string Language
    {
        get => settings.Language;
        set
        {
            if (settings.Language != value)
            {
                settings.Language = value;
                Save();
            }
        }
    }

    /// <summary>
    /// Gets or sets the API key.
    /// </summary>
    public static string APIKey
    {
        get => settings.APIKey;
        set
        {
            if (settings.APIKey != value)
            {
                settings.APIKey = value;
                Save();
            }
        }
    }

    /// <summary>
    /// Gets or sets the selected model for the application.
    /// </summary>
    public static string SelectedModel
    {
        get => settings.SelectedModel;
        set
        {
            if (settings.SelectedModel != value)
            {
                settings.SelectedModel = value;
                Save();
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to automatically send the prompt.
    /// </summary>
    public static bool AutoSendPrompt
    {
        get => settings.AutoSendPrompt;
        set
        {
            if (settings.AutoSendPrompt != value)
            {
                settings.AutoSendPrompt = value;
                Save();
            }
        }
    }

    /// <summary>
    /// Gets or sets the key for the hotkey.
    /// </summary>
    public static Key HotkeyKey
    {
        get => Enum.TryParse(settings.HotkeyKey, out Key key) ? key : Key.W;
        set
        {
            settings.HotkeyKey = value.ToString();
            Save();
        }
    }

    /// <summary>
    /// Gets or sets the modifiers for the hotkey.
    /// </summary>
    public static ModifierKeys HotkeyModifiers
    {
        get => Enum.TryParse(settings.HotkeyModifiers, out ModifierKeys mod) ? mod : (ModifierKeys.Control | ModifierKeys.Alt);
        set
        {
            settings.HotkeyModifiers = value.ToString();
            Save();
        }
    }

    /// <summary>
    /// Saves the settings to the user configuration file.
    /// </summary>
    public static void Save() => settings.Save();
}