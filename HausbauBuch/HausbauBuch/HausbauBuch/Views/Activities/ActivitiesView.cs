using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HausbauBuch.Controls;
using Xamarin.Forms;

namespace HausbauBuch.Views.Activities
{
    public class ActivitiesView : DefaultContentPage
    {
        public ActivitiesView()
        {
            var activitiesListView = new ListView
            {
                ItemTemplate = new DataTemplate(typeof (ActivitiesCell)),
                HasUnevenRows = true
            };
            activitiesListView.ItemTapped += ActivitiesListViewOnItemTapped;
            SetListViewItems(activitiesListView);

            var stack = new StackLayout();
            stack.Children.Add(activitiesListView);

            Content = stack;
        }

        private async void ActivitiesListViewOnItemTapped(object sender, ItemTappedEventArgs itemTappedEventArgs)
        {
            var activity = itemTappedEventArgs.Item as Classes.Activities;
            ((ListView) sender).SelectedItem = null;
            await Navigation.PushAsync(new ActivityView() {Activity = activity});
        }

        private async void SetListViewItems(ListView activitiesListView)
        {
            activitiesListView.ItemsSource = await App.ActivityController.Get(x => !x.Deleted, x => x.Date);
        }

    }
}
