using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HausbauBuch.Controls;
using HausbauBuch.Helper;
using HausbauBuch.Views.Home;
using Xamarin.Forms;

namespace HausbauBuch.Views.Activities
{
    public class ActivityView : DefaultContentPage
    {
        public static BindableProperty ActivityProperty = BindableProperty.Create("Activity", typeof(Classes.Activities), typeof(DefaultContentPage));

        public Classes.Activities Activity
        {
            get { return (Classes.Activities)GetValue(ActivityProperty); }
            set { SetValue(ActivityProperty, value); }
        }
        
        public ActivityView(Classes.Activities activity)
        {
            Activity = activity;

            BindingContext = Activity;

            Title = "Aufgabe";

            var titleLabel = new DefaultLabel
            {
                Text = "Grund"
            };

            var titleEntry = new DefaultEntry {Placeholder = "Grund"};
            titleEntry.SetBinding(Entry.TextProperty, new Binding("Title"));

            var dateLabel = new DefaultLabel
            {
                Text = "Erledigen bis"
            };

            var datePicker = new DatePicker();
            datePicker.SetBinding(DatePicker.DateProperty, new Binding("Date"));
            
            var descriptionEntry = new DefaultEntry {Placeholder = "Details"};
            descriptionEntry.SetBinding(Entry.TextProperty, new Binding("Description"));

            var finishLabel = new DefaultLabel
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Text = "Erledigt?"
            };

            var finishedSwitch = new Switch {HorizontalOptions = LayoutOptions.End};
            finishedSwitch.SetBinding(Switch.IsToggledProperty, new Binding("Finished"));

            var isCheckListLabel = new DefaultLabel
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Text = "Checkliste?"
            };

            var isCheckListSwitch = new Switch {HorizontalOptions = LayoutOptions.End};
            isCheckListSwitch.SetBinding(Switch.IsToggledProperty, new Binding("IsCheckList"));

            var finishStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                BackgroundColor = Colors.Primary,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    finishLabel,
                    finishedSwitch
                }
            };

            var isCheckListStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                BackgroundColor = Colors.Primary,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    isCheckListLabel,
                    isCheckListSwitch
                }
            };

            var stack = new StackLayout
            {
                Children =
                {
                    titleLabel,
                    titleEntry,
                    dateLabel,
                    datePicker,
                    isCheckListStack,
                    descriptionEntry,
                    finishStack
                }
            };

            Content = stack;
        }

        protected override void OnDisappearing()
        {
            SaveActivity();
            base.OnDisappearing();
        }
        
        private async void SaveActivity()
        {
            if (Activity.Id == null)
            {
                Activity.ModifiedAt = DateTime.Now;
                Activity.Id = await App.ActivityController.Insert(Activity);
                Dashboard.EntityLists.ActivityItems.Add(Activity);
            }
            else
            {
                await App.ActivityController.Update(Activity);
            }
        }
    }
}
