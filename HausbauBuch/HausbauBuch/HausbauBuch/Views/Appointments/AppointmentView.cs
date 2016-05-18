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
    public class AppointmentView : DefaultContentPage
    {
        public static BindableProperty AppointmentProperty = BindableProperty.Create("Appointment", typeof(Classes.Appointments), typeof(AppointmentView));

        public Classes.Appointments Appointment
        {
            get { return (Classes.Appointments) GetValue(AppointmentProperty); }
            set { SetValue(AppointmentProperty, value);}
        }

        public AppointmentView(Classes.Appointments appointment)
        {
            Appointment = appointment;
            BindingContext = Appointment;

            var titleLabel = new DefaultLabel { Text = "Terminname" };

            var titleEntry = new DefaultEntry {Placeholder = "Terminname"};
            titleEntry.SetBinding(Entry.TextProperty, new Binding("Title"));

            var detailLabel = new DefaultLabel { Text = "Details" };

            var detailEntry = new DefaultEntry {Placeholder = "Details"};
            detailEntry.SetBinding(Entry.TextProperty, new Binding("Detail"));

            var placeLabel = new DefaultLabel {Text = "Ort"};
            var placeEntry = new DefaultEntry {Placeholder = "Ort"};
            placeEntry.SetBinding(Entry.TextProperty, new Binding("Place"));
            
            var startTimePicker = new DefaultTimePicker {Format = "HH:mm"};
            startTimePicker.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == DefaultTimePicker.SelectedTimeProperty.PropertyName)
                {
                    Appointment.StartTime = ((DefaultTimePicker) sender).SelectedTime;
                }
            };
            startTimePicker.Time = new TimeSpan(Appointment.StartTime.Hour, Appointment.StartTime.Minute,0);

            var endTimePicker = new DefaultTimePicker {Format = "HH:mm"};
            endTimePicker.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == DefaultTimePicker.SelectedTimeProperty.PropertyName)
                {
                    Appointment.EndTime = ((DefaultTimePicker) sender).SelectedTime;
                }
            };
            endTimePicker.Time = new TimeSpan(Appointment.EndTime.Hour, Appointment.EndTime.Minute, 0);

            var startDatePicker = new DatePicker {Format = "dd.MM.yyyy"};
            startDatePicker.SetBinding(DatePicker.DateProperty, new Binding("StartDate"));
            startDatePicker.DateSelected += (sender, e) =>
            {
                Appointment.StartDate = e.NewDate;
            };

            var endDatePicker = new DatePicker {Format = "dd.MM.yyyy"};
            endDatePicker.SetBinding(DatePicker.DateProperty, new Binding("EndDate"));
            endDatePicker.DateSelected += (sender, e) =>
            {
                Appointment.EndDate = e.NewDate;
            };

            var finishToolbarItem = new ToolbarItem
            {
                Icon = "finish.png",
                Command = new Command(SaveAppointment)
            };

            ToolbarItems.Add(finishToolbarItem);

            var startTimeStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    startTimePicker,
                    startDatePicker
                }
            };

            var endTimeStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    endTimePicker,
                    endDatePicker
                }
            };

            var mainStack = new StackLayout
            {
                Children =
                {
                    titleLabel,
                    titleEntry,
                    placeLabel,
                    placeEntry,
                    startTimeStack,
                    endTimeStack,
                    detailLabel,
                    detailEntry
                }
            };

            Content = new ScrollView {Content = mainStack};
        }
        
        private async void SaveAppointment()
        {
            Appointment.CombinedStartDate = new DateTime(Appointment.StartDate.Year, Appointment.StartDate.Month, Appointment.StartDate.Day, Appointment.StartTime.Hour, Appointment.StartTime.Minute, Appointment.StartTime.Second);
            Appointment.CombinedEndDate = new DateTime(Appointment.EndDate.Year, Appointment.EndDate.Month, Appointment.EndDate.Day, Appointment.EndTime.Hour, Appointment.EndTime.Minute, Appointment.EndTime.Second);
            if (Appointment.Id == null)
            {
                Appointment.Id = await App.AppointmentsController.Insert(Appointment);
                Dashboard.EntityLists.AppointmentItems.Add(Appointment);
                Dashboard.Amounts.AppointmentsAmount++;
            }
            else
            {
                Appointment.ModifiedAt = DateTime.Now;
                await App.AppointmentsController.Update(Appointment);
            }
            await DisplayAlert("Erfolg", "Termin erfolgreich hinzugefügt", "Ok");
            MessagingCenter.Send(this, "update");
        }
    }
}
