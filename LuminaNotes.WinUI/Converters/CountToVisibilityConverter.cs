using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System;
using System.Collections;

namespace LuminaNotes.WinUI.Converters;

public class CountToVisibilityConverter : IValueConverter
{
    public bool IsInverted { get; set; } = false;

    public object Convert(object value, Type targetType, object parameter, string language)
    {
        var count = 0;

        if (value is int intValue)
            count = intValue;
        else if (value is ICollection collection)
            count = collection.Count;

        var hasItems = count > 0;
        if (IsInverted)
            hasItems = !hasItems;

        return hasItems ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
