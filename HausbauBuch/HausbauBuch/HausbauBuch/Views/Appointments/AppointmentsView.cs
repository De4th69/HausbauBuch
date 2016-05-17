using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HausbauBuch.Controls;
using HausbauBuch.Views.Home;
using Xamarin.Forms;

namespace HausbauBuch.Views.Appointments
{
    public class AppointmentsView : DefaultContentPage
    {
        private ListView _appointmentList;

        public AppointmentsView()
        {
            var calendarView = new CalendarView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HighlightedDays = Dashboard.EntityLists.AppointmentItems.Select(a => a.StartTime).ToList()
            };
            calendarView.DateSelected += OnDateSelected;

            _appointmentList = new ListView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HasUnevenRows = true,
                ItemTemplate = new DataTemplate(typeof(AppointmentCell)),
                ItemsSource = Dashboard.EntityLists.AppointmentItems.Where(a => a.StartDate == calendarView.SelectedDate)
            };
            _appointmentList.ItemTapped += AppointmentListOnItemTapped;

            var mainStack = new StackLayout
            {
                Children =
                {
                    calendarView,
                    _appointmentList
                }
            };

            var addToolbarItem = new ToolbarItem
            {
                Icon = "add.png",
                Command = new Command(async () =>
                {
                    var appointment = new Classes.Appointments {StartTime = calendarView.SelectedDate, EndTime = calendarView.SelectedDate.AddHours(1)};
                    await Navigation.PushAsync(new AppointmentView(appointment));
                })
            };

            ToolbarItems.Add(addToolbarItem);

            Content = mainStack;
        }

        private async void AppointmentListOnItemTapped(object sender, ItemTappedEventArgs itemTappedEventArgs)
        {
            var appointment = itemTappedEventArgs.Item as Classes.Appointments;
            ((ListView) sender).SelectedItem = null;
            await Navigation.PushAsync(new AppointmentView(appointment));
        }

        private void OnDateSelected(object sender, DateTime dateTime)
        {
            _appointmentList.ItemsSource = Dashboard.EntityLists.AppointmentItems.Where(a => a.StartDate == dateTime);
        }
    }
}