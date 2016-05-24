using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HausbauBuch.Controls;
using HausbauBuch.Helper;
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

        private readonly RemindMeValues _remindMeValues = new RemindMeValues();

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

            var startTimeLabel = new DefaultLabel {Text = "Von"};
            var startTimePicker = new DefaultTimePicker {Format = "HH:mm", HorizontalOptions = LayoutOptions.CenterAndExpand};

            var endTimeLabel = new DefaultLabel {Text = "Bis"};
            var endTimePicker = new DefaultTimePicker {Format = "HH:mm", HorizontalOptions = LayoutOptions.CenterAndExpand};

            var startDatePicker = new DatePicker {Format = "dd.MM.yyyy", HorizontalOptions = LayoutOptions.CenterAndExpand};
            startDatePicker.SetBinding(DatePicker.DateProperty, new Binding("StartDate"));

            var endDatePicker = new DatePicker {Format = "dd.MM.yyyy", HorizontalOptions = LayoutOptions.CenterAndExpand};
            endDatePicker.SetBinding(DatePicker.DateProperty, new Binding("EndDate"));

            var remindMeLabel = new DefaultLabel {Text = "Erinnerung"};
            var remindMePicker = new Picker {Title = "Erinnerung"};
            foreach (var remindMe in _remindMeValues.RemindMe)
            {
                remindMePicker.Items.Add(remindMe.Key);
            }
            remindMePicker.SelectedIndexChanged += RemindMePickerOnSelectedIndexChanged;

            var finishToolbarItem = new ToolbarItem
            {
                Icon = "finish.png",
                Command = new Command(SaveAppointment)
            };

            startTimePicker.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == DefaultTimePicker.SelectedTimeProperty.PropertyName)
                {
                    Appointment.StartTime = ((DefaultTimePicker)sender).SelectedTime;
                    endTimePicker.Time = new TimeSpan(((DefaultTimePicker) sender).Time.Hours + 1, 0, 0);
                }
            };
            startTimePicker.Time = new TimeSpan(Appointment.StartTime.Hour, Appointment.StartTime.Minute, 0);
            
            endTimePicker.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == DefaultTimePicker.SelectedTimeProperty.PropertyName)
                {
                    Appointment.EndTime = ((DefaultTimePicker)sender).SelectedTime;
                }
            };
            endTimePicker.Time = new TimeSpan(Appointment.EndTime.Hour, Appointment.EndTime.Minute, 0);

            startDatePicker.DateSelected += (sender, e) =>
            {
                Appointment.StartDate = e.NewDate;
                endDatePicker.Date = e.NewDate;
            };

            endDatePicker.DateSelected += (sender, e) =>
            {
                Appointment.EndDate = e.NewDate;
            };

            ToolbarItems.Add(finishToolbarItem);

            var startTimeStack = new StackLayout
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    startTimePicker,
                    startDatePicker
                }
            };

            var endTimeStack = new StackLayout
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
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
                    startTimeLabel,
                    startTimeStack,
                    endTimeLabel,
                    endTimeStack,
                    remindMeLabel,
                    remindMePicker,
                    detailLabel,
                    detailEntry
                }
            };

            Content = new ScrollView {Content = mainStack};
        }

        private void RemindMePickerOnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            var picker = (Picker) sender;
            if (picker.SelectedIndex != -1)
            {
                foreach(var remindMeValue in _remindMeValues.RemindMe.Where(r => r.Key == picker.Items[picker.SelectedIndex]))
                {
                    Appointment.Reminder = remindMeValue.Value;
                }
            }
        }

        private async void SaveAppointment()
        {
            Appointment.CombinedStartDate = new DateTime(Appointment.StartDate.Year, Appointment.StartDate.Month, Appointment.StartDate.Day, Appointment.StartTime.Hour, Appointment.StartTime.Minute, Appointment.StartTime.Second);
            Appointment.CombinedEndDate = new DateTime(Appointment.EndDate.Year, Appointment.EndDate.Month, Appointment.EndDate.Day, Appointment.EndTime.Hour, Appointment.EndTime.Minute, Appointment.EndTime.Second);
            
            if (Appointment.Reminder != 0)
            {
                var notificationService = Acr.Notifications.Notifications.Instance;
                var notification = new Acr.Notifications.Notification
                {
                    Date = Appointment.CombinedStartDate.AddMinutes(-Appointment.Reminder),
                    Message = $"Termin um {Appointment.CombinedStartDate}. {Appointment.Detail}",
                    Title = Appointment.Title,
                    Sound = "plop.mp3"
                };
                Appointment.NotificationId = notificationService.Send(notification);
            }

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