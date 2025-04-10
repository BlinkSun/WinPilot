using System.Windows;
using System.Windows.Input;
using WinPilot.Managers;
using WinPilot.Services;

namespace WinPilot.ViewModels;

/// <summary>
/// ViewModel for managing application settings.
/// </summary>
public class SettingsViewModel : ViewModelBase
{
    /// <summary>
    /// List of available keys for hotkey selection.
    /// </summary>
    public IReadOnlyList<Key> KeyOptions { get; } = [
        Key.A, Key.B, Key.C, Key.D, Key.E, Key.F, Key.G, Key.H, Key.I, Key.J,
        Key.K, Key.L, Key.M, Key.N, Key.O, Key.P, Key.Q, Key.R, Key.S, Key.T,
        Key.U, Key.V, Key.W, Key.X, Key.Y, Key.Z,

        Key.D0, Key.D1, Key.D2, Key.D3, Key.D4, Key.D5, Key.D6, Key.D7, Key.D8, Key.D9,

        Key.F1, Key.F2, Key.F3, Key.F4, Key.F5, Key.F6,
        Key.F7, Key.F8, Key.F9, Key.F10, Key.F11, Key.F12,

        Key.Insert, Key.Delete, Key.Home, Key.End,
        Key.PageUp, Key.PageDown,
        Key.Up, Key.Down, Key.Left, Key.Right,

        Key.Space, Key.Tab, Key.Enter, Key.Escape
    ];

    /// <summary>
    /// List of available ChatModels for selection.
    /// </summary>
    public IReadOnlyList<ChatModel> ChatModels { get; } = ChatModel.AllModels;

    /// <summary>
    /// Gets or sets the API key.
    /// </summary>
    public string APIKey
    {
        get => SettingsManager.APIKey;
        set
        {
            if (value != APIKey)
            {
                SettingsManager.APIKey = value;
                OnPropertyChanged(nameof(APIKey));
                OnPropertyChanged(nameof(IsApiKeyValid));
            }
        }
    }

    /// <summary>
    /// Gets a value indicating whether the API key is valid.
    /// </summary>
    public bool IsApiKeyValid => !string.IsNullOrWhiteSpace(APIKey) && APIKey.StartsWith("sk-");

    /// <summary>
    /// Gets or sets the selected ChatModel.
    /// </summary>
    public ChatModel SelectedModel
    {
        get => ChatModels.FirstOrDefault(m => m.ModelId == SettingsManager.SelectedModel) ?? ChatModel.Gpt4oMini;
        set
        {
            if (value != SelectedModel)
            {
                SettingsManager.SelectedModel = value.ModelId;
                OnPropertyChanged(nameof(SelectedModel));
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to automatically send the prompt. 
    /// </summary>
    public bool AutoSendPrompt
    {
        get => SettingsManager.AutoSendPrompt;
        set
        {
            if (value != AutoSendPrompt)
            {
                SettingsManager.AutoSendPrompt = value;
                OnPropertyChanged(nameof(AutoSendPrompt));
            }
        }
    }

    /// <summary>
    /// Gets or sets the selected hotkey key.
    /// </summary>
    public Key SelectedHotkeyKey
    {
        get => SettingsManager.HotkeyKey;
        set
        {
            if (value != SelectedHotkeyKey)
            {
                SettingsManager.HotkeyKey = value;
                OnPropertyChanged(nameof(SelectedHotkeyKey));
            }
        }
    }

    /// <summary>
    /// Gets or sets the selected Ctrl modifier keys for the hotkey.
    /// </summary>
    public bool ModifierCtrl
    {
        get => SettingsManager.HotkeyModifiers.HasFlag(ModifierKeys.Control);
        set
        {
            if (value != ModifierCtrl)
            {
                UpdateModifiers(nameof(ModifierCtrl), value);
            }
        }
    }

    /// <summary>
    /// Gets or sets the selected Alt modifier key for the hotkey.
    /// </summary>
    public bool ModifierAlt
    {
        get => SettingsManager.HotkeyModifiers.HasFlag(ModifierKeys.Alt);
        set
        {
            if (value != ModifierAlt)
            {
                UpdateModifiers(nameof(ModifierAlt), value);
            }
        }
    }

    /// <summary>
    /// Gets or sets the selected Shift modifier key for the hotkey.
    /// </summary>
    public bool ModifierShift
    {
        get => SettingsManager.HotkeyModifiers.HasFlag(ModifierKeys.Shift);
        set
        {
            if (value != ModifierShift)
            {
                UpdateModifiers(nameof(ModifierShift), value);
            }
        }
    }

    /// <summary>
    /// Gets or sets the selected Windows modifier key for the hotkey.
    /// </summary>
    public bool ModifierWin
    {
        get => SettingsManager.HotkeyModifiers.HasFlag(ModifierKeys.Windows);
        set
        {
            if (value != ModifierWin)
            {
                UpdateModifiers(nameof(ModifierWin), value);
            }
        }
    }

    /// <summary>
    /// Gets the list of supported languages for the application.
    /// </summary>
    public static IReadOnlyDictionary<string, string> Languages => LanguageManager.SupportedLanguages;

    /// <summary>
    /// Gets or sets the selected language for the application.
    /// </summary>
    public string SelectedLanguage
    {
        get => SettingsManager.Language;
        set
        {
            if (value != SelectedLanguage)
            {
                SettingsManager.Language = value;
                OnPropertyChanged(nameof(SelectedLanguage));
            }
        }
    }

    /// <summary>
    /// Updates the combined ModifierKeys based on selected checkboxes.
    /// Validates to ensure between 1 and 3 modifiers.
    /// </summary>
    private void UpdateModifiers(string source, bool isEnabled)
    {
        ModifierKeys combined = ModifierKeys.None;
        int count = 0;

        if (source != nameof(ModifierCtrl) && ModifierCtrl) { combined |= ModifierKeys.Control; count++; }
        else if (source == nameof(ModifierCtrl) && isEnabled) { combined |= ModifierKeys.Control; count++; }

        if (source != nameof(ModifierAlt) && ModifierAlt) { combined |= ModifierKeys.Alt; count++; }
        else if (source == nameof(ModifierAlt) && isEnabled) { combined |= ModifierKeys.Alt; count++; }

        if (source != nameof(ModifierShift) && ModifierShift) { combined |= ModifierKeys.Shift; count++; }
        else if (source == nameof(ModifierShift) && isEnabled) { combined |= ModifierKeys.Shift; count++; }

        if (source != nameof(ModifierWin) && ModifierWin) { combined |= ModifierKeys.Windows; count++; }
        else if (source == nameof(ModifierWin) && isEnabled) { combined |= ModifierKeys.Windows; count++; }

        if (count == 0)
        {
            MessageBox.Show(Resources.Strings.Msg_HotkeyNone, Resources.Strings.Msg_HotkeyTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (count > 3)
        {
            MessageBox.Show(Resources.Strings.Msg_HotkeyTooMany, Resources.Strings.Msg_HotkeyTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        SettingsManager.HotkeyModifiers = combined;

        OnPropertyChanged(nameof(ModifierCtrl));
        OnPropertyChanged(nameof(ModifierAlt));
        OnPropertyChanged(nameof(ModifierShift));
        OnPropertyChanged(nameof(ModifierWin));
    }
}