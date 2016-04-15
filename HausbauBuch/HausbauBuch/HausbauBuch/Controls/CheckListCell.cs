using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HausbauBuch.Helper;
using Xamarin.Forms;

namespace HausbauBuch.Controls
{
    public class CheckListCell : ViewCell
    {
        public CheckListCell()
        {
            var dateLabel = new DefaultLabel
            {
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            dateLabel.SetBinding(Label.TextProperty, new Binding("Date"));

            var nameLabel = new DefaultLabel
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            nameLabel.SetBinding(Label.TextProperty, new Binding("Name"));

            var checkSwitch = new Switch
            {
                BackgroundColor = Colors.Primary,
                HorizontalOptions = LayoutOptions.EndAndExpand
            };
            checkSwitch.SetBinding(Switch.IsToggledProperty, new Binding("Finished"));

            var cellStack = new StackLayout
            {
                BackgroundColor = Colors.Primary,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    dateLabel,
                    nameLabel,
                    checkSwitch
                }
            };
            
            View = cellStack;
        }
    }
}
