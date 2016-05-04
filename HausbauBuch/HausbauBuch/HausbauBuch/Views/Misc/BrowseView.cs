using HausbauBuch.Controls;
using Xamarin.Forms;

namespace HausbauBuch.Views.Misc
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
