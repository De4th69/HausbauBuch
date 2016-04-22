using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HausbauBuch.Helper;
using Xamarin.Forms;

namespace HausbauBuch.Controls
{
    public class ActivitiesCell : ViewCell
    {
        public ActivitiesCell()
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

            View = mainStack;
        }
    }
}
