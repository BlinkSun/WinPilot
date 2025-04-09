using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using WinPilot.Native;

namespace WinPilot.Helpers;

/// <summary>
/// Captures the visible portion of a window as a PNG byte array.
/// </summary>
public static class ScreenshotHelper
{
    /// <summary>
    /// Captures the content of a given window handle.
    /// </summary>
    public static byte[]? CaptureWindowAsByteArray(IntPtr hwnd)
    {
        if (!Win32Api.GetWindowRect(hwnd, out Win32Api.RECT rect))
            return null;

        int width = rect.Width;
        int height = rect.Height;

        using Bitmap bmp = new(width, height, PixelFormat.Format32bppArgb);
        using Graphics gfxBmp = Graphics.FromImage(bmp);
        IntPtr hdcBitmap = gfxBmp.GetHdc();

        bool succeeded = Win32Api.PrintWindow(hwnd, hdcBitmap, 0);
        gfxBmp.ReleaseHdc(hdcBitmap);

        if (!succeeded)
        {
            using Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(rect.Left, rect.Top, 0, 0, new Size(width, height), CopyPixelOperation.SourceCopy);
        }

        using MemoryStream ms = new();
        bmp.Save(ms, ImageFormat.Png);
        return ms.ToArray();
    }
}