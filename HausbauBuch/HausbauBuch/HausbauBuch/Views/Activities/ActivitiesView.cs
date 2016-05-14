using System.Linq;
using HausbauBuch.Controls;
using HausbauBuch.Views.Home;
using Xamarin.Forms;

namespace HausbauBuch.Views.Activities
{
    public class ActivitiesView : DefaultContentPage
    {
        private readonly ListView _activitiesListView;

        public ActivitiesView()
        {
            Title = "Aufgaben";

            _activitiesListView = new ListView
            {
                ItemTemplate = new DataTemplate(() =>
                {
                    var activityCell = new ActivitiesCell(_activitiesListView);
                    return activityCell;
                }),
                HasUnevenRows = true
            };
            _activitiesListView.ItemTapped += ActivitiesListViewOnItemTapped;
            _activitiesListView.Refreshing += (sender, args) => { SetListViewItems(); _activitiesListView.EndRefresh(); };
            SetListViewItems();

            var stack = new StackLayout();
            stack.Children.Add(_activitiesListView);

            var addToolbarItem = new ToolbarItem
            {
                Text = "Neu",
                Command = new Command(async () =>
                {
                    var activity = new Classes.Activities();
                    await Navigation.PushAsync(new ActivityView(activity));
                })
            };

            ToolbarItems.Add(addToolbarItem);
            Content = stack;
        }

        protected override void OnAppearing()
        {
            //SetListViewItems();
            MessagingCenter.Subscribe<ActivityView>(this, "update", view => SetListViewItems());
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<ActivityView>(this, "update");
            base.OnDisappearing();
        }

        private async void ActivitiesListViewOnItemTapped(object sender, ItemTappedEventArgs itemTappedEventArgs)
        {
            var activity = itemTappedEventArgs.Item as Classes.Activities;
            ((ListView) sender).SelectedItem = null;
            await Navigation.PushAsync(new ActivityView(activity));
        }

        private void SetListViewItems()
        {
            _activitiesListView.ItemsSource = Dashboard.EntityLists.ActivityItems.Where(a => !a.Deleted);
        }
    }
}
