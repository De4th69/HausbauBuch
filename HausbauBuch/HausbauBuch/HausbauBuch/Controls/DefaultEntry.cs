using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HausbauBuch.Helper;
using Xamarin.Forms;

namespace HausbauBuch.Controls
{
    public class DefaultEntry : Entry
    {
        public DefaultEntry()
        {
            FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            TextColor = Colors.PrimaryTextColor;
            BackgroundColor = Colors.Primary;
            Keyboard = Keyboard.Text;
        }
    }
}
