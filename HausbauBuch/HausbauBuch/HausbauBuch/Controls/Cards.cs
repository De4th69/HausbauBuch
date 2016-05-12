using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HausbauBuch.Classes;
using HausbauBuch.Helper;
using HausbauBuch.Views;
using HausbauBuch.Views.Home;
using Xamarin.Forms;

namespace HausbauBuch.Controls
{
    public class Cards : Frame
    {
        public static BindableProperty CardsPageProperty = BindableProperty.Create("CardsPage", typeof(CardPage), typeof(Cards),CardPage.Nothing);

        public CardPage CardsPage
        {
            get { return (CardPage) GetValue(CardsPageProperty); }
            set { SetValue(CardsPageProperty, value);}
        }
        
        public Cards(string title, string iconName = "", CardPage page = CardPage.Nothing)
        {
            var nameLabel = new DefaultLabel
            {
                Text = title,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.EndAndExpand
            };

            var icon = new Image
            {
                Source = iconName,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center
            };

            var amountLabel = new DefaultLabel
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalOptions = LayoutOptions.Center
            };

            switch (page)
            {
                case CardPage.Activities:
                    amountLabel.SetBinding(Label.TextProperty, new Binding("ActivitiesAmount"));
                    break;
                case CardPage.Appointments:
                    amountLabel.SetBinding(Label.TextProperty, new Binding("AppointmentsAmount"));
                    break;
                case CardPage.Contacts:
                    amountLabel.SetBinding(Label.TextProperty, new Binding("ContactsAmount"));
                    break;
                case CardPage.Documents:
                    amountLabel.SetBinding(Label.TextProperty, new Binding("DocumentsAmount"));
                    break;
                case CardPage.Enviroment:
                    amountLabel.SetBinding(Label.TextProperty, new Binding("EnviromentsAmount"));
                    break;
                case CardPage.Garden:
                    amountLabel.SetBinding(Label.TextProperty, new Binding("GardenAmount"));
                    break;
            }

            BindingContext = Dashboard.Amounts;

            BackgroundColor = Colors.Primary;
            
            CardsPage = page;
            Padding = 5;

            WidthRequest = 500;
            HeightRequest = 300;

            var upperStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    icon,
                    amountLabel
                }
            };

            var mainStack = new StackLayout
            {
                Children =
                {
                    upperStack,
                    nameLabel
                }
            };

            Content = mainStack;
        }
    }
}
