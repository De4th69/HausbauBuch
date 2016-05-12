using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HausbauBuch.Helper;
using HausbauBuch.Views;
using HausbauBuch.Views.Contacts;
using HausbauBuch.Views.Home;
using Plugin.Messaging;
using Xamarin.Forms;
using Softweb.Controls;

namespace HausbauBuch.Controls
{
    public class ContactCard : ViewCell
    {
        public ContactCard(ListView parentListView = null)
        {
            var nameLabel = new DefaultLabel
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            nameLabel.SetBinding(Label.TextProperty, new Binding("FullName"));

            var companyNameLabel = new DefaultLabel
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            companyNameLabel.SetBinding(Label.TextProperty, new Binding("CompanyName"));

            var emailLabel = new DefaultLabel
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            emailLabel.SetBinding(Label.TextProperty, new Binding("Email"));
            
            var contactImage = new Image
            {
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            contactImage.SetBinding(Image.SourceProperty, new Binding("ImagePath", BindingMode.Default, new ImagePathConverter()));

            var labelStack = new StackLayout
            {
                BackgroundColor = Colors.Primary,
                Children =
                {
                    nameLabel,
                    companyNameLabel,
                    emailLabel
                }
            };

            var mainStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    contactImage,
                    labelStack
                }
            };
            
            var deleteMenuItem = new MenuItem {Icon = "delete.png", Text = "Löschen"};
            deleteMenuItem.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            deleteMenuItem.Clicked += async (sender, e) =>
            {
                var contact = ((MenuItem) sender).CommandParameter as Classes.Contacts;
                if (contact != null)
                {
                    contact.Deleted = true;
                    contact.ModifiedAt = DateTime.Now;
                    Dashboard.Amounts.ContactsAmount--;
                    await App.ContactsController.Update(contact);
                    parentListView?.BeginRefresh();
                }
            };

            var browseMenuItem = new MenuItem {Icon = "browse.png", Text = "Im Browser"};
            browseMenuItem.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            browseMenuItem.Clicked += (sender, e) =>
            {
                var contact = ((MenuItem) sender).CommandParameter as Classes.Contacts;
                if (!string.IsNullOrEmpty(contact?.InternetAddress))
                {
                    MessagingCenter.Send(this, "browse", contact.InternetAddress);
                }
                else
                {
                    ShowError("Internetadresse");
                }
            };

            var emailMenuItem = new MenuItem {Icon = "mail.png", Text = "Email"};
            emailMenuItem.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            emailMenuItem.Clicked += (sender, e) =>
            {
                var contact = ((MenuItem) sender).CommandParameter as Classes.Contacts;
                if (!string.IsNullOrEmpty(contact?.Email))
                {
                    if (MessagingPlugin.EmailMessenger.CanSendEmail)
                    {
                        MessagingPlugin.EmailMessenger.SendEmail(contact.Email, "", "");
                    }
                }
                else
                {
                    ShowError("Email");
                }
            };

            var phoneMenuItem = new MenuItem {Icon = "call.png", Text = "Anrufen"};
            phoneMenuItem.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            phoneMenuItem.Clicked += (sender, e) =>
            {
                var contact = ((MenuItem) sender).CommandParameter as Classes.Contacts;
                if (!string.IsNullOrEmpty(contact?.PhoneNumber))
                {
                    if (MessagingPlugin.PhoneDialer.CanMakePhoneCall)
                    {
                        MessagingPlugin.PhoneDialer.MakePhoneCall(contact.PhoneNumber);
                    }
                }
                else
                {
                    ShowError("Telefonnummer");
                }
            };

            var mobileMenuItem = new MenuItem {Icon = "mobile.png", Text = "Handy anrufen"};
            mobileMenuItem.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            mobileMenuItem.Clicked += (sender, e) =>
            {
                var contact = ((MenuItem) sender).CommandParameter as Classes.Contacts;
                if (!string.IsNullOrEmpty(contact?.MobileNumber))
                {
                    if (MessagingPlugin.PhoneDialer.CanMakePhoneCall)
                    {
                        MessagingPlugin.PhoneDialer.MakePhoneCall(contact.MobileNumber);
                    }
                }
                else
                {
                    ShowError("Handynummer");
                }
            };

            ContextActions.Add(phoneMenuItem);
            ContextActions.Add(emailMenuItem);
            ContextActions.Add(browseMenuItem);
            ContextActions.Add(mobileMenuItem);
            ContextActions.Add(deleteMenuItem);

            View = new Frame {Content = mainStack, BackgroundColor = Colors.Primary, HasShadow = true};
        }

        private void ShowError(string type)
        {
            MessagingCenter.Send(this, "error", type);
        }
    }
}
