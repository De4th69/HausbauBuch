using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HausbauBuch.Controls;
using Xamarin.Forms;

namespace HausbauBuch.Views.Contacts
{
    public class BrowseView : DefaultContentPage
    {
        public BrowseView(string internetAddress)
        {
            var browser = new WebView {Source = internetAddress};
            Content = browser;
        }
    }
}
