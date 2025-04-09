using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WinPilot.Converters;

/// <summary>
/// Converts a boolean value representing API key validity into a border brush color.
/// </summary>
public class ApiKeyValidationBrushConverter : IValueConverter
{
    /// <summary>
    /// Converts a boolean validity into a brush.
    /// </summary>
    /// <param name="value">True if API key is valid, otherwise false.</param>
    /// <returns>A red brush if invalid, accent blue if valid.</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool isValid = value is bool b && b;
        return isValid ? new SolidColorBrush(Color.FromRgb(0, 120, 215)) : Brushes.Red;
    }

    /// <summary>
    /// Not used — one-way binding only.
    /// </summary>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}