using Foundation;
using HausbauBuch.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using TimePickerRenderer = HausbauBuch.iOS.TimePickerRenderer;

[assembly: ExportRenderer(typeof(DefaultTimePicker), typeof(TimePickerRenderer))]

namespace HausbauBuch.iOS
{
    public class TimePickerRenderer  : Xamarin.Forms.Platform.iOS.TimePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
        {
            base.OnElementChanged(e);

            var timePicker = (UIDatePicker) Control.InputView;
            timePicker.Locale = new NSLocale("de_DE");
        }
    }
}