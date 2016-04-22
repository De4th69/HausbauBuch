using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HausbauBuch.Business;
using HausbauBuch.Classes;
using HausbauBuch.Helper;
using Xamarin.Forms;

namespace HausbauBuch.Controls
{
    public class CheckListCell : ViewCell
    {
        public static readonly BindableProperty ParentListViewProperty = BindableProperty.Create("ParentListView", typeof(ListView), typeof(CheckListCell));

        public ListView ParentListView
        {
            get { return (ListView) GetValue(ParentListViewProperty); }
            set { SetValue(ParentListViewProperty, value);}
        }

        public CheckListCell()
        {
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
            
            var finishAction = new MenuItem {Text = "Fertig"};
            finishAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            finishAction.Clicked += async (sender, e) =>
            {
                var activity = ((MenuItem) sender).CommandParameter as Activities;
                if (activity != null)
                {
                    activity.Finished = true;
                    activity.FinishedOn = DateTime.Now;
                    await new ActivitiesController().Update(activity);
                    ParentListView.BeginRefresh();
                }
            };

            var deleteAction = new MenuItem {Text = "Löschen", IsDestructive = true};
            deleteAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            deleteAction.Clicked += async (sender, e) =>
            {
                var activity = ((MenuItem) sender).CommandParameter as Activities;
                if (activity != null)
                {
                    activity.Deleted = true;
                    activity.ModifiedAt = DateTime.Now;
                    await new ActivitiesController().Update(activity);
                    ParentListView.BeginRefresh();
                }
            };

            ContextActions.Add(finishAction);
            ContextActions.Add(deleteAction);

            View = cellStack;
        }
    }
}
