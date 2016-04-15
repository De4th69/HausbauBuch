using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HausbauBuch.Controls;
using Xamarin.Forms;

namespace HausbauBuch.Views.Contacts
{
    public class ContactsView : DefaultContentPage
    {
        public ContactsView()
        {
            Content = new StackLayout
            {
                Children =
                {
                    new DefaultLabel
                    {
                        Text = "Placeholder"
                    }
                }
            };
        }
    }
}
