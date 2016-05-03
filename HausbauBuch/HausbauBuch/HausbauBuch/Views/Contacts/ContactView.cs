using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HausbauBuch.Controls;
using HausbauBuch.Views.Home;
using Xamarin.Forms;

namespace HausbauBuch.Views.Contacts
{
    public class ContactView : DefaultContentPage
    {
        public static readonly BindableProperty ContactProperty = BindableProperty.Create("Contact", typeof(Classes.Contacts), typeof(ContactView));

        public Classes.Contacts Contact
        {
            get { return (Classes.Contacts) GetValue(ContactProperty); }
            set { SetValue(ContactProperty, value); }
        }

        public ContactView(Classes.Contacts contact)
        {
            Contact = contact;
            BindingContext = contact;

            Title = "Kontakt";

            var firstNameLabel = new DefaultLabel {Text = "Vorname"};
            var firstNameEntry = new DefaultEntry {Placeholder = "Vorname"};
            firstNameEntry.SetBinding(Entry.TextProperty, new Binding("FirstName"));

            var lastNameLabel = new DefaultLabel {Text = "Nachname"};
            var lastNameEntry = new DefaultEntry {Placeholder = "Nachname"};
            lastNameEntry.SetBinding(Entry.TextProperty, new Binding("LastName"));

            var companyNameLabel = new DefaultLabel {Text = "Firmenname"};
            var companyNameEntry = new DefaultEntry {Placeholder = "Firmenname"};
            companyNameEntry.SetBinding(Entry.TextProperty, new Binding("CompanyName"));

            var phoneNumberLabel = new DefaultLabel {Text = "Telefon"};
            var phoneNumberEntry = new DefaultEntry {Placeholder = "Tel", Keyboard = Keyboard.Telephone};
            phoneNumberEntry.SetBinding(Entry.TextProperty, new Binding("PhoneNumber"));

            var mobileNumberLabel = new DefaultLabel {Text = "Mobil"};
            var mobileNumberEntry = new DefaultEntry {Placeholder = "Mobil", Keyboard = Keyboard.Telephone};
            mobileNumberEntry.SetBinding(Entry.TextProperty, new Binding("MobileNumber"));

            var emailLabel = new DefaultLabel {Text = "Email"};
            var emailEntry = new DefaultEntry {Placeholder = "Email", Keyboard = Keyboard.Email};
            emailEntry.SetBinding(Entry.TextProperty, new Binding("Email"));

            var internetAddressLabel = new DefaultLabel {Text = "Internetadresse"};
            var internetAddressEntry = new DefaultEntry {Placeholder = "www", Keyboard = Keyboard.Url};
            internetAddressEntry.SetBinding(Entry.TextProperty, new Binding("InternetAddress"));

            var mainStack = new StackLayout
            {
                Children =
                {
                    firstNameLabel,
                    firstNameEntry,
                    lastNameLabel,
                    lastNameEntry,
                    companyNameLabel,
                    companyNameEntry,
                    phoneNumberLabel,
                    phoneNumberEntry,
                    mobileNumberLabel,
                    mobileNumberEntry,
                    emailLabel,
                    emailEntry,
                    internetAddressLabel,
                    internetAddressEntry
                }
            };

            //TODO: Add imageselection

            Content = new ScrollView {Content = mainStack};
        }

        protected override void OnDisappearing()
        {
            SaveContact();
            base.OnDisappearing();
        }

        private async void SaveContact()
        {
            Contact.FullName = Contact.FirstName + " " + Contact.LastName;
            if (Contact.Id == null)
            {
                Contact.Id = await App.ContactsController.Insert(Contact);
                Dashboard.EntityLists.ContactItems.Add(Contact);
                Dashboard.Amounts.ContactsAmount++;
            }
            else
            {
                Contact.ModifiedAt = DateTime.Now;
                await App.ContactsController.Update(Contact);
            }
        }
    }
}
