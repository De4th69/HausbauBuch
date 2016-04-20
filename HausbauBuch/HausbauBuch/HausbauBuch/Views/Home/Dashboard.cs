using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HausbauBuch.Business;
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
        private static readonly ActivitiesController ActivityController = new ActivitiesController();

        private readonly ListView _checkList;

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
            
            var appointmentsCard = new Cards("Termine", 12, "appointments.png", CardPage.Appointments);
            var appointmentsTapGestureRecognizer = new TapGestureRecognizer();
            appointmentsTapGestureRecognizer.Tapped += AppointmentsCardClickedAsync;
            appointmentsCard.GestureRecognizers.Add(appointmentsTapGestureRecognizer);
            var activitiesCard = new Cards("Aufgaben", 90, "activities.png", CardPage.Activities);
            var activitiesTapGestureRecognizer = new TapGestureRecognizer();
            activitiesTapGestureRecognizer.Tapped += ActivitiesCardClickedAsync;
            activitiesCard.GestureRecognizers.Add(activitiesTapGestureRecognizer);
            var documentsCard = new Cards("Dateien", 4, "documents.png", CardPage.Documents);
            var documentsTapGestureRecognizer = new TapGestureRecognizer();
            documentsTapGestureRecognizer.Tapped += DocumentsCardClickedAsync;
            documentsCard.GestureRecognizers.Add(documentsTapGestureRecognizer);
            var contactsCard = new Cards("Kontakte", 12, "contacts.png", CardPage.Contacts);
            var contactsTapGestureRecognizer = new TapGestureRecognizer();
            contactsTapGestureRecognizer.Tapped += ContactsCardClickedAsync;
            contactsCard.GestureRecognizers.Add(contactsTapGestureRecognizer);
            var enviromentsCard = new Cards("Einrichtung", 46, "enviroment.png", CardPage.Enviroment);
            var enviromentsTapGestureRecognizer = new TapGestureRecognizer();
            enviromentsTapGestureRecognizer.Tapped += EnviromentsCardClickedAsync;
            enviromentsCard.GestureRecognizers.Add(enviromentsTapGestureRecognizer);
            var gardensCard = new Cards("Garten", 11, "garden.png", CardPage.Garden);
            var gardensTapGestureRecognizer = new TapGestureRecognizer();
            gardensTapGestureRecognizer.Tapped += GardensCardClickedAsync;
            gardensCard.GestureRecognizers.Add(gardensTapGestureRecognizer);

            grid.Children.Add(appointmentsCard, 0, 0);
            grid.Children.Add(activitiesCard, 1, 0);
            grid.Children.Add(documentsCard, 0, 1);
            grid.Children.Add(contactsCard, 1, 1);
            grid.Children.Add(enviromentsCard, 0, 2);
            grid.Children.Add(gardensCard, 1, 2);
            
            _checkList = new ListView
            {
                BackgroundColor = Colors.Primary,
                ItemTemplate = new DataTemplate(typeof (CheckListCell))
            };
            SetCheckListItems();

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
                        //Command = new Command(async () => await Navigation.PushAsync(new CreateCheckListItemView()))
                        Command = new Command(CreateTestData)
                    }
                }
            };

            var stack = new StackLayout
            {
                Children =
                {
                    listViewHeaderStack,
                    _checkList
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

        private void CreateTestData()
        {
            var act1 = new Classes.Activities
            {
                Title = "Test1",
                Description = "testestestestsetsetes",
                IsCheckList = true
            };
            var act2 = new Classes.Activities
            {
                Title = "Test2",
                Description = "jkdsafjklsa",
                IsCheckList = true
            };

            act1.Id = ActivityController.SaveActivity(act1);
            act2.Id = ActivityController.SaveActivity(act2);
            SetCheckListItems();
        }
        
        private async Task NavigatePage(Cards card)
        {
            switch (card.CardsPage)
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

        private async void ActivitiesCardClickedAsync(object sender, EventArgs e)
        {
            await NavigatePage((Cards) sender);
        }

        private async void AppointmentsCardClickedAsync(object sender, EventArgs e)
        {
            await NavigatePage((Cards) sender);
        }

        private async void ContactsCardClickedAsync(object sender, EventArgs e)
        {
            await NavigatePage((Cards) sender);
        }

        private async void DocumentsCardClickedAsync(object sender, EventArgs e)
        {
            await NavigatePage((Cards) sender);
        }

        private async void EnviromentsCardClickedAsync(object sender, EventArgs e)
        {
            await NavigatePage((Cards) sender);
        }

        private async void GardensCardClickedAsync(object sender, EventArgs e)
        {
            await NavigatePage((Cards) sender);
        }

        private void SetCheckListItems()
        {
            var checkListItems = ActivityController.GetCheckListActivities();
            _checkList.ItemsSource = checkListItems;
        }
    }
}
