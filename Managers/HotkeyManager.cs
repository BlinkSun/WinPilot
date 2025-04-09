using System.Windows.Input;
using System.Windows.Interop;
using WinPilot.Native;

namespace WinPilot.Managers;

/// <summary>
/// Handles registration and triggering of system-wide hotkeys.
/// </summary>
public static class HotkeyManager
{
    private static HwndSource? source;
    private static Action? callback;

    /// <summary>
    /// Registers a global hotkey and associates a callback to it.
    /// </summary>
    public static void RegisterHotKey(Key key, ModifierKeys modifiers, Action onHotkeyPressed)
    {
        callback = onHotkeyPressed;

        HwndSourceParameters parameters = new("WinPilotHotkeySink")
        {
            WindowStyle = 0x800000, // WS_OVERLAPPED
            Width = 0,
            Height = 0
        };

        source = new(parameters);
        source.AddHook(WndProc);

        uint mod = (uint)modifiers;
        uint vk = (uint)KeyInterop.VirtualKeyFromKey(key);
        Win32Api.RegisterHotKey(source.Handle, Win32Api.HOTKEY_ID, mod, vk);
    }

    /// <summary>
    /// Rebinds the hotkey to a new key and modifiers, and updates the callback.
    /// </summary>
    public static void RebindHotKey(Key newKey, ModifierKeys newModifiers, Action onHotkeyPressed)
    {
        Unregister();
        RegisterHotKey(newKey, newModifiers, onHotkeyPressed);
    }

    /// <summary>
    /// Unregisters the previously registered hotkey.
    /// </summary>
    public static void Unregister()
    {
        if (source != null)
        {
            Win32Api.UnregisterHotKey(source.Handle, Win32Api.HOTKEY_ID);
            source.RemoveHook(WndProc);
            source = null;
        }
    }

    /// <summary>
    /// Processes the window messages for the registered hotkey.
    /// </summary>
    private static nint WndProc(nint hwnd, int msg, nint wParam, nint lParam, ref bool handled)
    {
        if (msg == Win32Api.WM_HOTKEY && wParam.ToInt32() == Win32Api.HOTKEY_ID)
        {
            callback?.Invoke();
            handled = true;
        }
        return IntPtr.Zero;
    }
}
