using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HausbauBuch.Controls;
using HausbauBuch.Views.Home;
using HausbauBuch.Views.Misc;
using Softweb.Controls;
using Xamarin.Forms;

namespace HausbauBuch.Views.Contacts
{
    public class ContactsView : DefaultContentPage
    {
        private readonly ListView _contactsListView;

        public ContactsView()
        {
            Title = "Kontakte";

            _contactsListView = new ListView
            {
                HasUnevenRows = true,
                ItemTemplate = new DataTemplate(() =>
                {
                    var contactCell = new ContactCard(_contactsListView);
                    return contactCell;
                })
            };
            _contactsListView.ItemTapped += ContactsListViewOnItemTapped;
            _contactsListView.Refreshing += (sender, args) => { SetListViewItems(); _contactsListView.EndRefresh(); };
            SetListViewItems();

            var mainStack = new StackLayout {Children = {_contactsListView}};

            var addToolbarItem = new ToolbarItem
            {
                Text = "Neu",
                Command = new Command(async () =>
                {
                    var contact = new Classes.Contacts();
                    await Navigation.PushAsync(new ContactView(contact));
                })
            };

            var importToolbarItem = new ToolbarItem
            {
                Text = "Import",
                Command = new Command(async () =>
                {
                    await Navigation.PushAsync(new SelectContactView());
                })
            };

            MessagingCenter.Subscribe<ContactCard, string>(this, "browse", (card, s) =>
            {
                NavigateToBrowser(s);
            });

            MessagingCenter.Subscribe<ContactView>(this, "update", view => SetListViewItems());

            MessagingCenter.Subscribe<ImportContactView>(this, "update", view => SetListViewItems());

            MessagingCenter.Subscribe<ContactCard, string>(this, "error", (card, t) =>
            {
                ShowError(t);
            });

            ToolbarItems.Add(addToolbarItem);
            ToolbarItems.Add(importToolbarItem);

            Content = mainStack;
        }
        
        private async void ContactsListViewOnItemTapped(object sender, ItemTappedEventArgs itemTappedEventArgs)
        {
            var contact = itemTappedEventArgs.Item as Classes.Contacts;
            ((ListView) sender).SelectedItem = null;
            await Navigation.PushAsync(new ContactView(contact));
        }

        private void SetListViewItems()
        {
            _contactsListView.ItemsSource = Dashboard.EntityLists.ContactItems.Where(c => !c.Deleted);
        }

        private async void NavigateToBrowser(string internetAddress)
        {
            await Navigation.PushAsync(new BrowseView(internetAddress));
        }

        private async void ShowError(string type)
        {
            await DisplayAlert("Fehler", $"Keine {type} angegeben", "OK");
        }
    }
}
