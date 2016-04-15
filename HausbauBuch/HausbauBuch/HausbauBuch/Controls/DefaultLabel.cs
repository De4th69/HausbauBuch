using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HausbauBuch.Helper;
using Xamarin.Forms;

namespace HausbauBuch.Controls
{
    public class DefaultLabel : Label
    {
        public DefaultLabel()
        {
            FontAttributes = FontAttributes.Bold;
            FontSize = Device.GetNamedSize(NamedSize.Medium, typeof (Label));
            HorizontalTextAlignment = TextAlignment.Center;
            VerticalTextAlignment = TextAlignment.Center;
            TextColor = Colors.PrimaryTextColor;
            BackgroundColor = Colors.Primary;
        }
    }
}
