using System.Windows;
using WinPilot.Native;

namespace WinPilot.Helpers;

/// <summary>
/// Provides access to the current mouse cursor position.
/// </summary>
public static class CursorHelper
{
    /// <summary>
    /// Gets the current screen position of the cursor.
    /// </summary>
    public static Point GetCursorPosition()
    {
        Win32Api.GetCursorPos(out Win32Api.POINT point);
        return new Point(point.X, point.Y);
    }
}