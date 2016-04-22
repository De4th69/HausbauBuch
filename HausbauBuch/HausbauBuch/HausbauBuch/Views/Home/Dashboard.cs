using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using HausbauBuch.Views.Contacts;
using HausbauBuch.Views.Documents;
using HausbauBuch.Views.Enviroment;
using HausbauBuch.Views.Garden;
using Xamarin.Forms;

namespace HausbauBuch.Views.Home
{
    public class Dashboard : DefaultContentPage
    {
        private readonly ListView _checkList;

        public static Amounts Amounts = new Amounts();

        public Dashboard()
        {
            InitAmounts();

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
            
            var appointmentsCard = new Cards("Termine", "appointments.png", CardPage.Appointments);
            var appointmentsTapGestureRecognizer = new TapGestureRecognizer();
            appointmentsTapGestureRecognizer.Tapped += AppointmentsCardClickedAsync;
            appointmentsCard.GestureRecognizers.Add(appointmentsTapGestureRecognizer);
            var activitiesCard = new Cards("Aufgaben", "activities.png", CardPage.Activities);
            var activitiesTapGestureRecognizer = new TapGestureRecognizer();
            activitiesTapGestureRecognizer.Tapped += ActivitiesCardClickedAsync;
            activitiesCard.GestureRecognizers.Add(activitiesTapGestureRecognizer);
            var documentsCard = new Cards("Dateien", "documents.png", CardPage.Documents);
            var documentsTapGestureRecognizer = new TapGestureRecognizer();
            documentsTapGestureRecognizer.Tapped += DocumentsCardClickedAsync;
            documentsCard.GestureRecognizers.Add(documentsTapGestureRecognizer);
            var contactsCard = new Cards("Kontakte", "contacts.png", CardPage.Contacts);
            var contactsTapGestureRecognizer = new TapGestureRecognizer();
            contactsTapGestureRecognizer.Tapped += ContactsCardClickedAsync;
            contactsCard.GestureRecognizers.Add(contactsTapGestureRecognizer);
            var enviromentsCard = new Cards("Einrichtung", "enviroment.png", CardPage.Enviroment);
            var enviromentsTapGestureRecognizer = new TapGestureRecognizer();
            enviromentsTapGestureRecognizer.Tapped += EnviromentsCardClickedAsync;
            enviromentsCard.GestureRecognizers.Add(enviromentsTapGestureRecognizer);
            var gardensCard = new Cards("Garten", "garden.png", CardPage.Garden);
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
                ItemTemplate = new DataTemplate(() =>
                {
                    var checkListCell = new CheckListCell {ParentListView = _checkList};
                    return checkListCell;
                })
            };

            SetCheckListItems();

            _checkList.Refreshing += (sender, args) =>
            {
                SetCheckListItems();
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

            var dropTables = new ToolbarItem
            {
                Text = "Drop",
                Command = new Command(DropTables)
            };
            ToolbarItems.Add(dropTables);
            Content = mainGrid;
        }

        private async void InitAmounts()
        {
            await Task.Run(async () =>
            {
                Amounts.ActivitiesAmount = await App.ActivityController.Count(x => !x.Deleted);
                Amounts.AppointmentsAmount = await App.AppointmentsController.Count(x => !x.Deleted);
                Amounts.ContactsAmount = await App.ContactsController.Count(x => !x.Deleted);
                Amounts.DocumentsAmount = await App.DocumentsController.Count(x => !x.Deleted);
                Amounts.EnviromentsAmount = await App.EnviromentsController.Count(x => !x.Deleted);
                Amounts.GardenAmount = await App.GardenController.Count(x => !x.Deleted);
            });
        }

        private async void DropTables()
        {
            //For Debug. Add new Tables to drop when new entities get introduced
            await App.SqlConnection.DropTableAsync<Classes.Activities>();
            await App.SqlConnection.CreateTableAsync<Classes.Activities>();
            await App.SqlConnection.DropTableAsync<Classes.Appointments>();
            await App.SqlConnection.CreateTableAsync<Classes.Appointments>();
            await App.SqlConnection.DropTableAsync<Classes.Contacts>();
            await App.SqlConnection.CreateTableAsync<Classes.Contacts>();
            await App.SqlConnection.DropTableAsync<Classes.Documents>();
            await App.SqlConnection.CreateTableAsync<Classes.Documents>();
            await App.SqlConnection.DropTableAsync<Classes.Enviroments>();
            await App.SqlConnection.CreateTableAsync<Classes.Enviroments>();
            await App.SqlConnection.DropTableAsync<Classes.GardenIdeas>();
            await App.SqlConnection.CreateTableAsync<Classes.GardenIdeas>();
            InitAmounts();
            SetCheckListItems();
        }
        
        private async void CreateTestData()
        {
            var act1 = new Classes.Activities
            {
                Title = "Test1",
                Description = "testestestestsetsetes",
                IsCheckList = true,
                Date = DateTime.Today
            };
            var act2 = new Classes.Activities
            {
                Title = "Test2",
                Description = "jkdsafjklsa",
                IsCheckList = true,
                Date = new DateTime(2016, 4, 27)
            };
            var act3 = new Classes.Activities
            {
                Title = "Test3",
                Description = "fsdjakflsajfklas",
                Date = new DateTime(2016, 4, 23)
            };

            act1.Id = await App.ActivityController.Insert(act1);
            Amounts.ActivitiesAmount++;
            act2.Id = await App.ActivityController.Insert(act2);
            act3.Id = await App.ActivityController.Insert(act3);
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

        public async void SetCheckListItems()
        {
            _checkList.ItemsSource = await App.ActivityController.Get(a => a.IsCheckList && !a.Deleted && !a.Finished, a => a.Date);
            _checkList.EndRefresh();
        }
    }
}
