using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HausbauBuch.Classes;
using HausbauBuch.Controls;
using HausbauBuch.Helper;
using HausbauBuch.Views.Activities;
using HausbauBuch.Views.Appointments;
using HausbauBuch.Views.CheckList;
using HausbauBuch.Views.Contacts;
using HausbauBuch.Views.Documents;
using HausbauBuch.Views.Enviroment;
using HausbauBuch.Views.Garden;
using Xamarin.Forms;

namespace HausbauBuch.Views.Home
{
    public class Dashboard : DefaultContentPage
    {
        public Dashboard()
        {
            Title = "Unser Hausbau-Buch";
            
            var grid = new Grid
            {
                ColumnSpacing = 10,
                RowDefinitions =
                {
                    new RowDefinition {Height = new GridLength(1, GridUnitType.Star)},
                    new RowDefinition {Height = new GridLength(1, GridUnitType.Star)},
                    new RowDefinition {Height = new GridLength(1, GridUnitType.Star)},
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)},
                    new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)}
                }
            };

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += async delegate { await NavigatePage(); };

            grid.Children.Add(new Cards("Termine", 12, "appointments.png"), 0, 0);
            grid.Children.Add(new Cards("Aufgaben", 90, "activities.png"), 1, 0);
            grid.Children.Add(new Cards("Dateien", 4, "documents.png"), 0, 1);
            grid.Children.Add(new Cards("Kontakte", 12, "contacts.png"), 1, 1);
            grid.Children.Add(new Cards("Einrichtung", 46, "enviroment.png"), 0, 2);
            grid.Children.Add(new Cards("Garten", 11, "garden.png"), 1, 2);
            
            var testData = new List<CheckListItem>
            {
                new CheckListItem
                {
                    Date = DateTime.Today.ToString(),
                    Name = "test1",
                    Finished = false
                },
                new CheckListItem
                {
                    Date = new DateTime(2016, 7, 14).ToString(),
                    Name = "test2",
                    Finished = true
                }
            };

            var checkList = new ListView
            {
                BackgroundColor = Colors.Primary,
                ItemTemplate = new DataTemplate(typeof (CheckListCell)),
                ItemsSource = testData
            };

            var listViewHeaderStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Colors.Primary,
                Children =
                {
                    new DefaultLabel {Text = "Checkliste", HorizontalOptions = LayoutOptions.CenterAndExpand},
                    new DefaultButton
                    {
                        HorizontalOptions = LayoutOptions.EndAndExpand,
                        BackgroundColor = Colors.Primary,
                        BorderRadius = 0,
                        BorderWidth = 0,
                        Image = "add.png",
                        Command = new Command(async () => await Navigation.PushAsync(new CreateCheckListItemView()))
                    }
                }
            };

            var stack = new StackLayout
            {
                Children =
                {
                    listViewHeaderStack,
                    checkList
                },
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };

            var mainGrid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition {Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition {Height = new GridLength(1, GridUnitType.Star) }
                },
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            mainGrid.Children.Add(grid,0,0);
            mainGrid.Children.Add(stack,0,1);
            Content = mainGrid;
        }

        private async Task NavigatePage(CardPage page = CardPage.Nothing)
        {
            switch (page)
            {
                case CardPage.Activities:
                    await Navigation.PushAsync(new ActivitiesView());
                    break;
                case CardPage.Appointments:
                    await Navigation.PushAsync(new AppointmentsView());
                    break;
                case CardPage.Contacts:
                    await Navigation.PushAsync(new ContactsView());
                    break;
                case CardPage.Documents:
                    await Navigation.PushAsync(new DocumentsView());
                    break;
                case CardPage.Enviroment:
                    await Navigation.PushAsync(new EnviromentView());
                    break;
                case CardPage.Garden:
                    await Navigation.PushAsync(new GardenView());
                    break;
            }
        }
    }
}
