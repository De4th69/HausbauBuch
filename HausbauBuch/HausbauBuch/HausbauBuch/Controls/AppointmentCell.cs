using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HausbauBuch.Classes;
using HausbauBuch.Helper;
using HausbauBuch.Views;
using HausbauBuch.Views.Home;
using Xamarin.Forms;

namespace HausbauBuch.Controls
{
    public class AppointmentCell : ViewCell
    {
        public AppointmentCell()
        {
            var startTimeLabel = new DefaultLabel
            {
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            startTimeLabel.SetBinding(Label.TextProperty, new Binding("CombinedStartDate", BindingMode.Default, new TimeConverter()));

            var endTimeLabel = new DefaultLabel
            {
                HorizontalOptions = LayoutOptions.EndAndExpand
            };
            endTimeLabel.SetBinding(Label.TextProperty, new Binding("CombinedEndDate", BindingMode.Default, new TimeConverter()));

            var placeHolderLabel = new DefaultLabel {Text = " - "};

            var titleLabel = new DefaultLabel
            {
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            titleLabel.SetBinding(Label.TextProperty, new Binding("Title"));

            var placeLabel = new DefaultLabel
            {
                HorizontalOptions = LayoutOptions.EndAndExpand
            };
            placeLabel.SetBinding(Label.TextProperty, new Binding("Place"));

            var upperStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    titleLabel,
                    placeLabel
                }
            };

            var lowerStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    startTimeLabel,
                    placeHolderLabel,
                    endTimeLabel
                }
            };

            var deleteAction = new MenuItem {Icon = "delete.png"};
            deleteAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            deleteAction.Clicked += async (sender, e) =>
            {
                var appointment = ((MenuItem) sender).CommandParameter as Appointments;
                if (appointment != null)
                {
                    appointment.Deleted = true;
                    appointment.ModifiedAt = DateTime.Now;
                    if (appointment.Reminder != 0)
                    {
                        Acr.Notifications.Notifications.Instance.Cancel(appointment.NotificationId);
                    }
                    Dashboard.EntityLists.AppointmentItems.Remove(appointment);
                    Dashboard.Amounts.AppointmentsAmount--;
                    await App.AppointmentsController.Update(appointment);
                }
            };

            ContextActions.Add(deleteAction);

            var mainStack = new StackLayout
            {
                BackgroundColor = Colors.Primary,
                Children =
                {
                    upperStack,
                    lowerStack
                },
                Padding = 5
            };
            
            View = mainStack;
        }
    }
}
