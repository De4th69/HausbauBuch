using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HausbauBuch.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using TimePicker = Xamarin.Forms.TimePicker;

[assembly: ExportRenderer(typeof(DefaultTimePicker), typeof(HausbauBuch.Droid.TimePickerRenderer))]

namespace HausbauBuch.Droid
{
    public class TimePickerRenderer : ViewRenderer<Xamarin.Forms.TimePicker, Android.Widget.EditText>, TimePickerDialog.IOnTimeSetListener
    {
        private TimePickerDialog _timePicker = null;

        protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
        {
            base.OnElementChanged(e);
            SetNativeControl(new Android.Widget.EditText(Forms.Context));
            Control.Click += ControlOnClick;
            var time = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, Element.Time.Hours, Element.Time.Minutes, 0);
            Control.Text = time.ToString("HH:mm");
            Control.KeyListener = null;
            Control.FocusChange += ControlOnFocusChanged;
        }

        private void ControlOnFocusChanged(object sender, FocusChangeEventArgs e)
        {
            if (e.HasFocus)
            {
                ShowTimePicker();
            }
        }

        private void ControlOnClick(object sender, EventArgs e)
        {
            ShowTimePicker();
        }

        private void ShowTimePicker()
        {
            if (_timePicker == null)
            {
                _timePicker = new TimePickerDialog(Forms.Context, this, Element.Time.Hours, Element.Time.Minutes, true);
            }
            _timePicker.Show();
        }

        public void OnTimeSet(Android.Widget.TimePicker view, int hourOfDay, int minute)
        {
            var time = new TimeSpan(hourOfDay, minute, 0);
            Element.SetValue(TimePicker.TimeProperty, time);
            var timeDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, hourOfDay, minute, 0);
            Control.Text = timeDate.ToString("HH:mm");
        }
    }
}