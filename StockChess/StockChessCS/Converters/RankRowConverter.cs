using System;
using System.Globalization;
using System.Windows.Data;

namespace StockChessCS.Converters
{
    public class RankRowConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var rank = (int) value;
            var row = 8 - rank;
            return row;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
