using System.Windows;
using WinPilot.Controllers;
using WinPilot.Managers;

namespace WinPilot.Views;

/// <summary>
/// Logique d'interaction pour SettingsWindow.xaml
/// </summary>
public partial class SettingsWindow : Window
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsWindow"/> class.
    /// </summary>
    public SettingsWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Handles the Click event of the Save button.
    /// </summary>
    private void OnCloseClicked(object sender, RoutedEventArgs e)
    {
        SettingsManager.Save();
        HotkeyManager.RebindHotKey(
            SettingsManager.HotkeyKey,
            SettingsManager.HotkeyModifiers,
            AppController.TriggerWinPilot
        );
        LanguageManager.ApplyLanguage(SettingsManager.Language);
        Close();
    }

    /// <summary>
    /// Handles the Click event of the Shutdown button.
    /// </summary>
    private void OnShutdownClicked(object sender, RoutedEventArgs e)
    {
        SettingsManager.Save();
        HotkeyManager.Unregister();
        Application.Current.Shutdown();
    }
}
