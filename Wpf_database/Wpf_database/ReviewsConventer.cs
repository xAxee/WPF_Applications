using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Wpf_database {
    [ValueConversion(typeof(int), typeof(string))]
    public class ReviewsConventer : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            int val = (int)value;
            string s = "";
            for(int i =0; i < val; i++) {
                s += "*";
            }
            return s;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            string s = (string)value;
            if (s.Count() > 10) return 10;
            if (s.Count() == 0) return 1;
            return s.Count();
        }
    }
}
