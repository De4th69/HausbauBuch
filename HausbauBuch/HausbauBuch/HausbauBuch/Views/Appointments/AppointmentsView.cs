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
        private readonly ListView _appointmentsListView;

        public AppointmentsView()
        {
            Title = "Termine";

            _appointmentsListView = new ListView
            {

            };
        }
    }
}
