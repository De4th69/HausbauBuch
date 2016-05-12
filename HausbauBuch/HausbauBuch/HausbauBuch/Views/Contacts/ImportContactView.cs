using System;
using System.Linq;
using HausbauBuch.Controls;
using HausbauBuch.Views.Home;
using Xamarin.Forms;

namespace HausbauBuch.Views.Contacts
{
    public class ImportContactView : DefaultContentPage
    {
        private readonly Classes.Contacts _contactToSave = new Classes.Contacts();

        public ImportContactView(Plugin.Contacts.Abstractions.Contact contact)
        {
            BindingContext = _contactToSave;

            var nameLabel = new DefaultLabel {Text = contact.DisplayName};

            var firstNameEntry = new DefaultEntry
            {
                Placeholder = "Vorname"
            };
            firstNameEntry.TextChanged += FirstNameEntryOnTextChanged;

            var lastNameEntry = new DefaultEntry
            {
                Placeholder = "Nachname"
            };
            lastNameEntry.TextChanged += LastNameEntryOnTextChanged;

            var companyEntry = new DefaultEntry
            {
                Placeholder = "Firmenname"
            };
            companyEntry.TextChanged += CompanyEntryOnTextChanged;

            var phoneLabel = new DefaultLabel
            {
                Text = "Telefon"
            };
            
            var phonePicker = new Picker {Title = "Telefon" };
            foreach (var phoneNumbers in contact.Phones)
            {
                phonePicker.Items.Add(phoneNumbers.Number);
            }
            phonePicker.SelectedIndexChanged += PhonePickerOnSelectedIndexChanged;

            var mobilePicker = new Picker {Title = "Mobil"};
            foreach (var mobileNumbers in contact.Phones)
            {
                mobilePicker.Items.Add(mobileNumbers.Number);
            }
            mobilePicker.SelectedIndexChanged += MobilePickerOnSelectedIndexChanged;

            var emailPicker = new Picker {Title = "Email"};
            foreach (var emailAdresses in contact.Emails)
            {
                emailPicker.Items.Add(emailAdresses.Address);
            }
            emailPicker.SelectedIndexChanged += EmailPickerOnSelectedIndexChanged;

            var internetAddressPicker = new Picker {Title = "Internetadresse"};
            foreach (var websites in contact.Websites)
            {
                internetAddressPicker.Items.Add(websites.Address);
            }
            internetAddressPicker.SelectedIndexChanged += InternetAddressPickerOnSelectedIndexChanged;

            var mainStack = new StackLayout {Children =
            {
                nameLabel,
                firstNameEntry,
                lastNameEntry,
                companyEntry,
                phonePicker,
                mobilePicker,
                emailPicker,
                internetAddressPicker
            }};

            firstNameEntry.Text = contact.FirstName;
            lastNameEntry.Text = contact.LastName;
            if (contact.Organizations.Count > 0)
            {
                companyEntry.Text = contact.Organizations.First().Name;
            }

            var saveToolbarItem = new ToolbarItem
            {
                Icon = "finish.png",
                Command = new Command(SaveContact)
            };

            ToolbarItems.Add(saveToolbarItem);

            Content = mainStack;
        }

        private void CompanyEntryOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            _contactToSave.CompanyName = textChangedEventArgs.NewTextValue;
        }

        private void LastNameEntryOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            _contactToSave.LastName = textChangedEventArgs.NewTextValue;
        }

        private void FirstNameEntryOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            _contactToSave.FirstName = textChangedEventArgs.NewTextValue;
        }

        private void InternetAddressPickerOnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            if (((Picker) sender).SelectedIndex != -1)
            {
                _contactToSave.InternetAddress = ((Picker) sender).Items[((Picker) sender).SelectedIndex];
            }
        }

        private void EmailPickerOnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            if (((Picker)sender).SelectedIndex != -1)
            {
                _contactToSave.Email = ((Picker)sender).Items[((Picker)sender).SelectedIndex];
            }
        }

        private void MobilePickerOnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            if (((Picker)sender).SelectedIndex != -1)
            {
                _contactToSave.MobileNumber = ((Picker)sender).Items[((Picker)sender).SelectedIndex];
            }
        }

        private void PhonePickerOnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            if (((Picker)sender).SelectedIndex != -1)
            {
                _contactToSave.PhoneNumber = ((Picker)sender).Items[((Picker)sender).SelectedIndex];
            }
        }

        private async void SaveContact()
        {
            _contactToSave.FullName = _contactToSave.FirstName + " " + _contactToSave.LastName;
            _contactToSave.Id = await App.ContactsController.Insert(_contactToSave);
            Dashboard.EntityLists.ContactItems.Add(_contactToSave);
            Dashboard.Amounts.ContactsAmount++;
            MessagingCenter.Send(this, "update");
            await Navigation.PopAsync();
        }
    }
}
