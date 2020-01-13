using System;
using System.Linq;
using System.Globalization;
using System.Windows.Data;

namespace StockChessCS.Converters
{
    public class FileColumnConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var file = (char) value;
            var files = ("abcdefgh").ToArray();
            var column = files.ToList().IndexOf(file);
            return column;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
