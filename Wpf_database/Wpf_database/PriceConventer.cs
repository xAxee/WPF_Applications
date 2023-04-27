using System;
using System.Globalization;
using System.Windows.Data;

namespace Wpf_database {
    [ValueConversion(typeof(decimal), typeof(decimal))]
    public class PriceConventer : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            decimal p = (decimal)value;
            return p + "$";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            string str = (string)value;
            if (str.Length == 0) return 0.00m;
            str = str.Substring(0, str.IndexOf("$") == -1 ? str.Length : str.IndexOf("$"));
            try {
                return decimal.Parse(str);
            } catch (Exception ex) { return 0.00m; }
        }
    }
}
