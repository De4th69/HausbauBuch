using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HausbauBuch.Controls;
using HausbauBuch.Views.Home;
using Plugin.Media;
using Plugin.Media.Abstractions;
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

            var photoToolbarItem = new ToolbarItem
            {
                Icon = "camera.png",
                Command = new Command(ShowActionSheet)
            };

            var saveToolbarItem = new ToolbarItem
            {
                Icon = "finish.png",
                Command = new Command(SaveContact)
            };

            ToolbarItems.Add(photoToolbarItem);
            ToolbarItems.Add(saveToolbarItem);

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
            
            Content = new ScrollView {Content = mainStack};
        }

        private async void SaveContact()
        {
            Contact.FullName = Contact.FirstName + " " + Contact.LastName;
            if (Contact.Id == null)
            {
                
                Contact.Id = await App.ContactsController.Insert(Contact);
                Dashboard.EntityLists.ContactItems.Add(Contact);
                Dashboard.Amounts.ContactsAmount++;
                MessagingCenter.Send(this, "update");
            }
            else
            {
                Contact.ModifiedAt = DateTime.Now;
                await App.ContactsController.Update(Contact);
            }
            await DisplayAlert("Erfolg", "Kontakt erfolgreich gespeichert", "Ok");
        }

        private async void ShowActionSheet()
        {
            await CrossMedia.Current.Initialize();

            var answer = await DisplayActionSheet("Foto", "Abbrechen", null, "Auswählen", "Aufnehmen");

            switch (answer)
            {
                case "Auswählen":
                    PickPhoto();
                    break;
                case "Aufnehmen":
                    TakePhoto();
                    break;
            }
        }

        private async void PickPhoto()
        {
            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                var photo = await CrossMedia.Current.PickPhotoAsync();
                if (photo != null)
                {
                    Contact.ImagePath = photo.Path;
                }
            }
            else
            {
                await DisplayAlert("Fehler", "Kann keine Fotos auswählen", "Ok");
            }
        }

        private async void TakePhoto()
        {
            if (CrossMedia.Current.IsTakePhotoSupported)
            {
                var photo =
                    await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                    {
                        DefaultCamera = CameraDevice.Rear,
                        Directory = "Photos",
                        Name = "hausbaubuch" + DateTime.Now,
                        PhotoSize = PhotoSize.Small
                    });

                if (photo != null)
                {
                    Contact.ImagePath = photo.Path;
                }
            }
            else
            {
                await DisplayAlert("Fehler", "Kann keine Fotos aufnehmen.", "Ok");
            }
        }
    }
}
