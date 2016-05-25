using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HausbauBuch.Helper;
using Xamarin.Forms;

namespace HausbauBuch.Controls
{
    public class DocumentsCell : ViewCell
    {
        public DocumentsCell()
        {
            var icon = new Image {HorizontalOptions = LayoutOptions.StartAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand, Source = "documents.png"};
            
            var nameLabel = new DefaultLabel();
            nameLabel.SetBinding(Label.TextProperty, new Binding("Name"));

            var sizeLabel = new DefaultLabel();
            sizeLabel.SetBinding(Label.TextProperty, new Binding("Size", BindingMode.Default, new ByteConverter()));

            var typeLabel = new DefaultLabel {TextColor = Colors.TextColorRed};
            typeLabel.SetBinding(Label.TextProperty, new Binding("DocumentType", BindingMode.Default, new MimeTypeConverter()));

            var upperLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    icon,
                    nameLabel,
                    typeLabel
                }
            };

            var mainLayout = new StackLayout
            {
                Children =
                {
                    upperLayout,
                    sizeLabel
                }
            };

            View = new Frame {BackgroundColor = Colors.Primary, Content = mainLayout};
        }
    }
}
