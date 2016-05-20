using System;
using System.Linq;
using System.Threading.Tasks;
using Acr.Notifications;
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

        public static EntityLists EntityLists = new EntityLists();

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
                    var activityCell = new ActivitiesCell(_checkList);
                    return activityCell;
                }),
                HasUnevenRows = true
            };
            
            _checkList.Refreshing += CheckListOnRefreshing;
            
            _checkList.ItemTapped += CheckListOnItemTapped;

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
                        Command = new Command(async () =>
                        {
                            var activity = new Classes.Activities {IsCheckList = true};
                            await Navigation.PushAsync(new ActivityView(activity));
                        })
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

        protected override void OnAppearing()
        {
            _checkList.BeginRefresh();
            InitAmounts();
            base.OnAppearing();
        }

        private async void CheckListOnRefreshing(object sender, EventArgs eventArgs)
        {
            EntityLists.ActivityItems = await App.ActivityController.Get(a => !a.Deleted, a => a.Date);
            EntityLists.AppointmentItems = await App.AppointmentsController.Get(a => !a.Deleted, a => a.Id);
            EntityLists.ContactItems = await App.ContactsController.Get(a => !a.Deleted, a => a.Id);
            EntityLists.DocumentItems = await App.DocumentsController.Get(a => !a.Deleted, a => a.Id);
            EntityLists.EnviromentItems = await App.EnviromentsController.Get(a => !a.Deleted, a => a.Id);
            EntityLists.GardenItems = await App.GardenController.Get(a => !a.Deleted, a => a.Id);
            _checkList.ItemsSource = EntityLists.ActivityItems.Where(a => a.IsCheckList && !a.Finished);
            _checkList.EndRefresh();
        }

        private async void CheckListOnItemTapped(object sender, ItemTappedEventArgs itemTappedEventArgs)
        {
            var activity = itemTappedEventArgs.Item as Classes.Activities;
            ((ListView) sender).SelectedItem = null;
            await Navigation.PushAsync(new ActivityView(activity));
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
            _checkList.BeginRefresh();
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
    }
}
