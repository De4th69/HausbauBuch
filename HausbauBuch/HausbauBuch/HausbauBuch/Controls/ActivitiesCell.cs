using System;
using HausbauBuch.Classes;
using HausbauBuch.Helper;
using HausbauBuch.Views;
using HausbauBuch.Views.Home;
using Xamarin.Forms;

namespace HausbauBuch.Controls
{
    public class ActivitiesCell : ViewCell
    {
        public static BindableProperty ParentListViewProperty = BindableProperty.Create("ParentListView", typeof(ListView), typeof(ActivitiesCell), null);

        public ListView ParentListView
        {
            get { return (ListView) GetValue(ParentListViewProperty); }
            set { SetValue(ParentListViewProperty, value); }
        }

        public ActivitiesCell(ListView parentListView = null)
        {
            if (parentListView != null)
            {
                ParentListView = parentListView;
            }

            var dateLabel = new DefaultLabel
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            dateLabel.SetBinding(Label.TextProperty, new Binding("Date", BindingMode.Default, new DateTimeConverter()));

            var nameLabel = new DefaultLabel
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            nameLabel.SetBinding(Label.TextProperty, new Binding("Title"));

            var cellStack = new StackLayout
            {
                BackgroundColor = Colors.Primary,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    dateLabel,
                    nameLabel
                }
            };

            var descriptionLabel = new DefaultLabel();
            descriptionLabel.SetBinding(Label.TextProperty, new Binding("Description"));

            var mainStack = new StackLayout
            {
                BackgroundColor = Colors.Primary,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    cellStack,
                    descriptionLabel
                }
            };

            var finishAction = new MenuItem {Icon = "finish.png"};
            finishAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            finishAction.Clicked += async (sender, e) =>
            {
                var activity = ((MenuItem) sender).CommandParameter as Activities;
                if (activity != null)
                {
                    activity.Finished = true;
                    activity.ModifiedAt = DateTime.Now;
                    activity.FinishedOn = DateTime.Now;
                    await App.ActivityController.Update(activity);
                    ParentListView?.BeginRefresh();
                }
            };

            var deleteAction = new MenuItem {Icon = "delete.png"};
            deleteAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            deleteAction.Clicked += async (sender, e) =>
            {
                var activity = ((MenuItem) sender).CommandParameter as Activities;
                if (activity != null)
                {
                    activity.Deleted = true;
                    activity.ModifiedAt = DateTime.Now;
                    Dashboard.Amounts.ActivitiesAmount--;
                    await App.ActivityController.Update(activity);
                }
            };

            ContextActions.Add(finishAction);
            ContextActions.Add(deleteAction);

            View = mainStack;
        }
    }
}
