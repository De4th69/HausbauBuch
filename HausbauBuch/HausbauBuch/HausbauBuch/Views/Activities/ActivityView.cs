using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HausbauBuch.Controls;
using Xamarin.Forms;
using Label = Xamarin.Forms.Label;

namespace HausbauBuch.Views.Activities
{
    public class ActivityView : DefaultContentPage
    {
        public Classes.Activities Activity { get; set; }

        public ActivityView()
        {
            var nameLabel = new DefaultLabel();
            nameLabel.SetBinding(Label.TextProperty, new Binding("Title"));


            BindingContext = Activity;

            var stack = new StackLayout
            {
                Children =
                {
                    nameLabel
                }
            };

            Content = stack;
        }
    }
}
