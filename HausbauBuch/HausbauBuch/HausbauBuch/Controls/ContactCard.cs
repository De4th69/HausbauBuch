using HausbauBuch.Classes;
using HausbauBuch.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HausbauBuch.Controls
{
    public class ContactCard : ViewCell
    {        
        public ContactCard()
        {
            var nameLabel = new DefaultLabel
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            nameLabel.SetBinding(Label.TextProperty, new Binding("FullName"));

            var phoneNumberLabel = new DefaultLabel
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            phoneNumberLabel.SetBinding(Label.TextProperty, new Binding("PhoneNumber"));

            var mobileLabel = new DefaultLabel
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            mobileLabel.SetBinding(Label.TextProperty, new Binding("Mobile"));

            var emailLabel = new DefaultLabel
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            emailLabel.SetBinding(Label.TextProperty, new Binding("Email"));

            var websiteLabel = new DefaultLabel
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            websiteLabel.SetBinding(Label.TextProperty, new Binding("Website"));

            var image = new Image
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            image.SetBinding(Image.SourceProperty, new Binding("ImagePath", BindingMode.Default, new ImagePathConverter()));


        }
    }
}
