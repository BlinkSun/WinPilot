using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Automation;
using WinPilot.Helpers;
using WinPilot.Native;

namespace WinPilot.Models;

/// <summary>
/// Captures a snapshot of the user's current context, including process, window title, focus, selection and screenshot.
/// </summary>
public class ContextSnapshot
{
    public IntPtr WindowHandle { get; set; } = IntPtr.Zero;
    public string ProcessName { get; set; } = string.Empty;
    public string WindowTitle { get; set; } = string.Empty;
    public string FocusedText { get; set; } = string.Empty;
    public string SelectedText { get; set; } = string.Empty;
    public byte[] ScreenshotBytes { get; set; } = [];

    /// <summary>
    /// Captures the current context snapshot: process, selection, window title, focused control, and screenshot.
    /// </summary>
    public static async Task<ContextSnapshot> Capture()
    {
        ContextSnapshot snapshot = new();

        // 1. Get foreground window handle
        IntPtr hwnd = Win32Api.GetForegroundWindow();
        snapshot.WindowHandle = hwnd;
        if (hwnd == IntPtr.Zero)
            return snapshot;

        // 2. Try to get selected text via simulated Ctrl+C
        string originalClipboard = Clipboard.GetText();
        Win32Api.SetForegroundWindow(hwnd);
        await Task.Delay(250); // let the window activate
        CopyPasteHelper.Copy();
        await Task.Delay(250); // give time to copy
        if (Clipboard.ContainsText())
        {
            string copied = Clipboard.GetText();
            if (!string.IsNullOrWhiteSpace(copied) && copied != originalClipboard)
            {
                snapshot.SelectedText = copied;
                Clipboard.SetText(originalClipboard); // restore original clipboard
            }
        }

        // 3. Process name
        _ = Win32Api.GetWindowThreadProcessId(hwnd, out uint pid);
        try
        {
            snapshot.ProcessName = Process.GetProcessById((int)pid).ProcessName;
        }
        catch
        {
            snapshot.ProcessName = "unknown";
        }

        // 4. Window title
        StringBuilder titleBuilder = new(256);
        _ = Win32Api.GetWindowText(hwnd, titleBuilder, titleBuilder.Capacity);
        snapshot.WindowTitle = titleBuilder.ToString();

        // 5. Focused element text (via UI Automation)
        try
        {
            AutomationElement? focusedElement = AutomationElement.FocusedElement;
            if (focusedElement != null)
            {
                if (focusedElement.TryGetCurrentPattern(ValuePattern.Pattern, out object? patternObj) && patternObj is ValuePattern valuePattern)
                {
                    snapshot.FocusedText = valuePattern.Current.Value;
                }
                else
                {
                    snapshot.FocusedText = focusedElement.Current.Name;
                }
            }
        }
        catch
        {
            snapshot.FocusedText = string.Empty;
        }

        // 6. Screenshot of the window
        snapshot.ScreenshotBytes = ScreenshotHelper.CaptureWindowAsByteArray(hwnd) ?? [];

        return snapshot;
    }

    /// <summary>
    /// Generates a context-rich system prompt for AI use, based on the captured state.
    /// </summary>
    public string ToSystemPrompt()
    {
        StringBuilder sb = new();

        sb.AppendLine("You are a silent and highly focused assistant.");
        sb.AppendLine("You do not ask questions. You do not chat.");
        sb.AppendLine("You only return the most relevant suggestion as output,");
        sb.AppendLine("based on what the user is doing or about to do, using the contextual clues provided.");
        sb.AppendLine();
        sb.AppendLine("Context:");
        sb.AppendLine($"• Application: {ProcessName}");
        sb.AppendLine($"• Window title: {WindowTitle}");

        if (!string.IsNullOrWhiteSpace(FocusedText))
            sb.AppendLine($"• Current focus: {FocusedText.Trim()}");

        //if (!string.IsNullOrWhiteSpace(SelectedText))
        //    sb.AppendLine($"• Selected text: {SelectedText.Trim()}");

        sb.AppendLine();
        sb.AppendLine("You may also receive a screenshot of the interface.");
        sb.AppendLine("From all this, you must deduce what the user is trying to do.");
        sb.AppendLine();
        sb.AppendLine("⛔️ Do not explain your reasoning.");
        sb.AppendLine("⛔️ Do not introduce or comment your answer.");
        sb.AppendLine("⛔️ Do not use Markdown, code blocks, or formatting like ```sql or **bold**.");
        sb.AppendLine("✅ Output only the raw suggestion, in plain text, ready to paste as-is.");
        sb.AppendLine("✅ Keep it brief, correct, and directly usable (e.g., short text, a code snippet or command).");

        return sb.ToString();
    }

    /// <summary>
    /// Returns a human-readable debug string containing the current context snapshot data.
    /// </summary>
    public string ToDebugString()
    {
        StringBuilder sb = new();

        sb.AppendLine("🧠 WinPilot Context Snapshot");
        sb.AppendLine("---------------------------");
        sb.AppendLine($"🪟 Window Handle : 0x{WindowHandle.ToString("X")}");
        sb.AppendLine($"🧾 Process Name  : {ProcessName}");
        sb.AppendLine($"📌 Window Title  : {WindowTitle}");

        if (!string.IsNullOrWhiteSpace(FocusedText))
            sb.AppendLine($"🔤 Focused Text  : {FocusedText.Trim()}");

        if (!string.IsNullOrWhiteSpace(SelectedText))
            sb.AppendLine($"📋 Selected Text : {SelectedText.Trim()}");

        sb.AppendLine($"🖼 Screenshot    : {(ScreenshotBytes?.Length > 0 ? $"{ScreenshotBytes.Length} bytes" : "none")}");

        return sb.ToString();
    }
}