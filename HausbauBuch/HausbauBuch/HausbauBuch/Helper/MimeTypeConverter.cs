using System;
using System.Globalization;
using HausbauBuch.Classes;
using Xamarin.Forms;

namespace HausbauBuch.Helper
{
    public class MimeTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new MimeType().Pdf;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
