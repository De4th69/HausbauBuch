using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HausbauBuch.Helper;
using Xamarin.Forms;

namespace HausbauBuch.Controls
{
    public class Cards : Grid
    {
        public static BindableProperty CardsPageProperty = BindableProperty.CreateAttached("CardsPage", typeof(CardPage), typeof(Cards),CardPage.Nothing, BindingMode.OneWay);

        public CardPage CardsPage
        {
            get { return (CardPage) GetValue(CardsPageProperty); }
            set { SetValue(CardsPageProperty, value);}
        }

        public Cards(string title, int amount = 0, string iconName = "", CardPage page = CardPage.Nothing)
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
                Text = amount.ToString(),
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalOptions = LayoutOptions.Center
            };

            BackgroundColor = Colors.Primary;
            ColumnDefinitions.Add(new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)});
            ColumnDefinitions.Add(new ColumnDefinition {Width = new GridLength(3, GridUnitType.Star)});
            RowDefinitions.Add(new RowDefinition {Height = new GridLength(2, GridUnitType.Star)});
            RowDefinitions.Add(new RowDefinition {Height = new GridLength(1, GridUnitType.Star)});

            CardsPage = page;

            Children.Add(icon, 0, 0);
            Children.Add(amountLabel, 1, 0);
            Children.Add(nameLabel, 0, 1);

            SetRow(nameLabel, 1);
            SetColumnSpan(nameLabel, 2);

            Padding = 5;

            WidthRequest = 500;
            HeightRequest = 300;
        }
    }
}
