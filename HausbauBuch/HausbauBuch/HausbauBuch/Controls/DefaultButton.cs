using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HausbauBuch.Helper;
using Xamarin.Forms;

namespace HausbauBuch.Controls
{
    public class DefaultButton : Button
    {
        public DefaultButton()
        {
            HorizontalOptions = LayoutOptions.FillAndExpand;
            VerticalOptions = LayoutOptions.FillAndExpand;
        }
    }
}
