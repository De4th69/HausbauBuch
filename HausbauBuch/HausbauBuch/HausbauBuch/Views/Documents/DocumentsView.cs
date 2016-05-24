using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLToolkit.Forms.Controls;
using HausbauBuch.Controls;
using HausbauBuch.Views.Home;
using Xamarin.Forms;

namespace HausbauBuch.Views.Documents
{
    public class DocumentsView : DefaultContentPage
    {
        public DocumentsView()
        {
            var flowListView = new FlowListView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HasUnevenRows = true,
                FlowColumnsTemplates = new List<FlowColumnTemplateSelector>
                {
                    new FlowColumnSimpleTemplateSelector {ViewType = typeof (TestView)},
                    new FlowColumnSimpleTemplateSelector {ViewType = typeof (TestView)},
                    new FlowColumnSimpleTemplateSelector {ViewType = typeof (TestView)}
                },
                FlowItemsSource = Dashboard.EntityLists.ContactItems
            };

            Content = flowListView;
        }
    }

    class TestView : ContentView
    {
        public TestView()
        {
            HorizontalOptions = LayoutOptions.FillAndExpand;
            VerticalOptions = LayoutOptions.FillAndExpand;
            var titleLabel = new Label();
            titleLabel.SetBinding(Label.TextProperty, new Binding("FullName"));

            Content = titleLabel;
        }
    }
}
