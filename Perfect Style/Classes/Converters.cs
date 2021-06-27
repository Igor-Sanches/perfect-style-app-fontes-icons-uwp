using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Perfect_Style.Classes
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            //Suppose ArtistName and songName are the properties of Song class
            return ((bool)value== true) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
