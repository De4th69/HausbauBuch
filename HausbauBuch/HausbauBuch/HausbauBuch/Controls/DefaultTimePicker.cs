using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HausbauBuch.Controls
{
    public class DefaultTimePicker : TimePicker
    {
        public static readonly BindableProperty SelectedTimeProperty = BindableProperty.Create("SelectedTime", typeof(DateTime), typeof(DefaultTimePicker), DateTime.Now);

        public DateTime SelectedTime
        {
            get { return (DateTime) GetValue(SelectedTimeProperty); }
            set { SetValue(SelectedTimeProperty, value);}
        }
    }
}
