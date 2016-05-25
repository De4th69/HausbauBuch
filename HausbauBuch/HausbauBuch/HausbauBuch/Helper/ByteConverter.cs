using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HausbauBuch.Helper
{
    public class ByteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                string[] suffix = {@"Byte/s", @"KByte/s", @"MByte/s", @"GByte/s",@"TB",@"PB",@"EB"};
                long bytes;
                long.TryParse(value.ToString(), out bytes);

                if (bytes == 0)
                {
                    return "0" + suffix[0];
                }

                var bytesCount = (ulong) Math.Abs((decimal) bytes);
                var suffixIndex = System.Convert.ToInt32(Math.Floor(Math.Log(bytesCount, 1024ul)));
                var num = Math.Round(bytesCount/Math.Pow(1024ul, suffixIndex), 1);
                return (Math.Sign((decimal)bytes)+num).ToString(CultureInfo.InvariantCulture) + " " + suffix[suffixIndex];
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
