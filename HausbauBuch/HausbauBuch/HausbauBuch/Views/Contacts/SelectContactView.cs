using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HausbauBuch.Controls;
using Plugin.Contacts;
using Plugin.Contacts.Abstractions;
using Xamarin.Forms;

namespace HausbauBuch.Views.Contacts
{
    public class SelectContactView : DefaultContentPage
    {
        private readonly ListView _contactsListView;

        private readonly ActivityIndicator _activityIndicator;

        public SelectContactView()
        {
            Title = "Kontakt importieren";

            _activityIndicator = new ActivityIndicator {IsRunning = false, IsVisible = false};

            _contactsListView = new ListView
            {
                ItemTemplate = new DataTemplate(() =>
                {
                    var textCell = new TextCell();
                    textCell.SetBinding(TextCell.TextProperty, new Binding("DisplayName"));
                    textCell.SetBinding(TextCell.DetailProperty, new Binding("Phones"));
                    return textCell;
                })
            };
            _contactsListView.ItemTapped += ContactsListViewOnItemTapped;

            var mainStack = new StackLayout {Children = { _activityIndicator, _contactsListView}};

            Content = mainStack;
        }

        protected override void OnAppearing()
        {
            GetContacts();
            base.OnAppearing();
        }

        private async void GetContacts()
        {
            _activityIndicator.IsVisible = true;
            _activityIndicator.IsRunning = true;
            if (await CrossContacts.Current.RequestPermission())
            {
                IQueryable<Contact> contacts = null;

                CrossContacts.Current.PreferContactAggregation = false;
                await Task.Run(() =>
                {
                    if (CrossContacts.Current.Contacts == null) return;
                    contacts = CrossContacts.Current.Contacts.Where(c => !string.IsNullOrEmpty(c.DisplayName));
                });

                _contactsListView.ItemsSource = contacts.OrderBy(c => c.DisplayName);
            }
            _activityIndicator.IsRunning = false;
            _activityIndicator.IsVisible = false;
        }

        private async void ContactsListViewOnItemTapped(object sender, ItemTappedEventArgs itemTappedEventArgs)
        {
            var contact = itemTappedEventArgs.Item as Contact;
            ((ListView) sender).SelectedItem = null;
            await Navigation.PushAsync(new ImportContactView(contact));
        }
    }
}
