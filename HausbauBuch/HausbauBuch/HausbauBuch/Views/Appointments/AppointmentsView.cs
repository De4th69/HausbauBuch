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
            DateSelected += OnDateSelected;
        }

        private async void OnDateSelected(object sender, DateTime dateTime)
        {
            await DisplayAlert("Date", dateTime.ToString(), "ok");
        }

        public void NotifyDateSelected(DateTime dateSelected)
        {
            DateSelected?.Invoke(this, dateSelected);
        }

        public event EventHandler<DateTime> DateSelected;
    }
}
