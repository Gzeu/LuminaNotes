using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System;

namespace LuminaNotes.WinUI.Converters;

public class NullToVisibilityConverter : IValueConverter
{
    public bool IsInverted { get; set; } = false;

    public object Convert(object value, Type targetType, object parameter, string language)
    {
        var isNull = value == null;
        if (IsInverted)
        {
            return isNull ? Visibility.Visible : Visibility.Collapsed;
        }
        return isNull ? Visibility.Collapsed : Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
