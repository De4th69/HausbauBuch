using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using HausbauBuch.Views;

namespace HausbauBuch.Droid
{
    [Activity(Label = "Hausbau-Buch", Icon = "@drawable/icon", Theme="@style/StandardTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            ToolbarResource = Resource.Layout.Toolbar;
            TabLayoutResource = Resource.Layout.TabLayout;

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}

