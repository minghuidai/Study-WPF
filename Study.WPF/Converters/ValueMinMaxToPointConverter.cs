using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Study.WPF.Converters
{
    public class ValueMinMaxToPointConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double value = (double)values[0];
            double minumum = (double)values[1];
            double maximum = (double)values[2];

            // Convert the value to one between 0 and 360
            double current = (value / (maximum - minumum)) * 360;

            // adjest the finished state so the ArcSegment gets drawn as a whole circle
            if (current == 360) current = 359.999;

            // shift by 90 degress so 0 starts at the top of the circle
            current = current - 90;

            // convert the angle to radians
            current = current * 0.017453292519943295;

            // calculate the circle's point
            double x = 10 + 10 * Math.Cos(current);
            double y = 10 + 10 * Math.Sin(current);
            return new Point(x, y);

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
