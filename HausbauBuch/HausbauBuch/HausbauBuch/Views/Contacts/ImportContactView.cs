using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HausbauBuch.Controls;
using Plugin.Contacts;
using Xamarin.Forms;

namespace HausbauBuch.Views.Contacts
{
    public class ImportContactView : DefaultContentPage
    {
        private ListView _contactsListView;

        public ImportContactView()
        {
            _contactsListView = new ListView
            {
                ItemTemplate = new DataTemplate(() =>
                {
                    var textCell = new TextCell();
                    textCell.SetBinding(TextCell.TextProperty, new Binding("LastName"));
                    textCell.SetBinding(TextCell.DetailProperty, new Binding("FirstName"));
                    return textCell;
                })
            };
            _contactsListView.ItemTapped += ContactsListViewOnItemTapped;

            var mainStack = new StackLayout {Children = {_contactsListView}};

            Content = mainStack;
        }

        protected override void OnAppearing()
        {
            GetContacts();
            base.OnAppearing();
        }

        private async void GetContacts()
        {
            if (await CrossContacts.Current.RequestPermission())
            {
                //TODO: Exceptionhandling
                CrossContacts.Current.PreferContactAggregation = false;
                await Task.Run(() =>
                {
                    if (CrossContacts.Current.Contacts == null) return;
                    var contacts = CrossContacts.Current.Contacts.Where(c => !string.IsNullOrEmpty(c.LastName)).ToList();
                    _contactsListView.ItemsSource = contacts.OrderBy(c => c.LastName);
                });
            }
        }

        private void ContactsListViewOnItemTapped(object sender, ItemTappedEventArgs itemTappedEventArgs)
        {
            
        }
    }
}
