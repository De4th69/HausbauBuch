using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HausbauBuch.Helper;
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
            startTimeLabel.SetBinding(Label.TextProperty, new Binding("CombinedStartTime", BindingMode.Default, new TimeConverter()));

            var endTimeLabel = new DefaultLabel
            {
                HorizontalOptions = LayoutOptions.EndAndExpand
            };
            endTimeLabel.SetBinding(Label.TextProperty, new Binding("CombinedEndTime", BindingMode.Default, new TimeConverter()));

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

            var mainStack = new StackLayout
            {
                BackgroundColor = Colors.Primary,
                Children =
                {
                    upperStack,
                    lowerStack
                }
            };

            View = mainStack;
        }
    }
}
