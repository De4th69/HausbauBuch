using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HausbauBuch.Helper
{
    public class TimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            if(value != null)
            {
                var time = value.ToString();
                DateTime parsedTime;
                DateTime.TryParse(time, out parsedTime);
                return parsedTime != DateTime.MinValue ? parsedTime.ToString("HH:mm") : string.Empty;
            }
            else
            {
                return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            throw new NotImplementedException();
        }
    }
}
