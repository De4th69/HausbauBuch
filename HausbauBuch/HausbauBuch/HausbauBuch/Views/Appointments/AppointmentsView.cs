using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HausbauBuch.Controls;
using Xamarin.Forms;

namespace HausbauBuch.Views.Appointments
{
    public class AppointmentsView : DefaultContentPage
    {
        public AppointmentsView()
        {
            Content = new StackLayout
            {
                Children =
                {
                    new DefaultLabel
                    {
                        Text = "Placeholder"
                    }
                }
            };
        }
    }
}
