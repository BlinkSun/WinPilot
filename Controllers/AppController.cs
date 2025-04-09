using System.Windows;
using WinPilot.Managers;
using WinPilot.Models;
using WinPilot.Views;

namespace WinPilot.Controllers;

/// <summary>
/// Centralized controller for core app-wide actions like triggering the WinPilot window.
/// </summary>
public static class AppController
{
    /// <summary>
    /// Captures the current context and opens the WinPilot popup.
    /// </summary>
    public static async void TriggerWinPilot()
    {
        ContextSnapshot context = await ContextSnapshot.Capture();
        Application.Current.Dispatcher.Invoke(() => new WinPilotWindow(context).Show());
    }

    /// <summary>
    /// Checks if the OpenAI key is valid. If not, it shows the settings window.
    /// Shutsdown the application if the key is still invalid.
    /// </summary>
    public static void CheckOpenAIKey()
    {
        if (string.IsNullOrWhiteSpace(SettingsManager.OpenAIKey) ||
                !SettingsManager.OpenAIKey.StartsWith("sk-"))
        {
            UIController.ShowSettingsWindow();
        }
    }

    /// <summary>
    /// Handles the application shutdown process.
    /// </summary>
    public static void ShutdownApp()
    {
        SettingsManager.Save();
        HotkeyManager.Unregister();
        Application.Current.Shutdown();
    }
}