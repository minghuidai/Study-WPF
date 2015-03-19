using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Study.WPF.Converters
{
    public class ValueMinMaxToIsLargeArcConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double value = (double)values[0];
            double minumum = (double)values[1];
            double maximum = (double)values[2];

            // Only rturn true if the value is 50% of the range or greater
            return ((value * 2) >= (maximum - minumum));

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
