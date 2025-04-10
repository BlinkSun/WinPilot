using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using WinPilot.Controllers;
using WinPilot.Managers;
using WinPilot.Models;
using WinPilot.Services;
using WinPilot.ViewModels;

namespace WinPilot.Views;

/// <summary>
/// Interaction logic for WinPilotWindow.xaml
/// </summary>
public partial class WinPilotWindow : Window
{
    /// <summary>
    /// Constructor for the WinPilotWindow.
    /// </summary>
    public WinPilotWindow(ContextSnapshot contextSnapshot)
    {
        InitializeComponent();
        DataContext = new WinPilotViewModel(contextSnapshot);
        Storyboard storyboard = (Storyboard)FindResource("CopilotProgressStoryboard");
        storyboard.Begin();
    }

    /// <summary>
    /// Event handler for when the window is loaded.
    /// </summary>
    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        UIController.PositionWindowNearCursor(this);
        Activate();
        UIController.FadeIn(this);
        TxtPrompt.Focus();
    }

    /// <summary>
    /// Event handler for when the user presses a key in the prompt text box.
    /// </summary>
    private void TxtPrompt_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            WinPilotViewModel? vm = DataContext as WinPilotViewModel;
            if (SettingsManager.AutoSendPrompt)
            {
                e.Handled = true;
                vm?.AcceptCommand.Execute(null);
            }
            else
            {
                ChatModel chatModel = ChatModel.AllModels.FirstOrDefault(m => m.ModelId == SettingsManager.SelectedModel) ?? ChatModel.Gpt4oMini;
                Task.Run(() => vm?.GenerateSuggestionAsync(chatModel));
            }
        }
    }
}