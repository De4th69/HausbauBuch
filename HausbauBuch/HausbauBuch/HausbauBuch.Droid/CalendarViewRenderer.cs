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
using HausbauBuch.Droid;
using HausbauBuch.Views.Appointments;
using Square.TimesSquare;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Xamarin.Forms.View;

[assembly: ExportRenderer(typeof(AppointmentsView), typeof(CalendarViewRenderer))]

namespace HausbauBuch.Droid
{
    public class CalendarViewRenderer : PageRenderer
    {
        private const string TAG = "Xamarin.Forms.Calendar";
        
        private CalendarPickerView _pickerView;

        private Android.Views.View _view;

        public CalendarViewRenderer()
        {
            
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            if (Element != null)
            {
                var element = (AppointmentsView) Element;
                var inflatorService = (LayoutInflater) Context.GetSystemService(Context.LayoutInflaterService);
                _view = (LinearLayout) inflatorService.Inflate(Resource.Layout.CalendarView, null, false);

                _pickerView = _view.FindViewById<CalendarPickerView>(Resource.Id.calendar_view);

                var highlightedDates = new List<DateTime>
                {
                    new DateTime(2016, 5, 12),
                    new DateTime(2016, 6, 13)
                };

                _pickerView.Init(DateTime.Now.AddYears(-2), DateTime.Now.AddYears(2))
                    .WithSelectedDate(DateTime.Today)
                    .InMode(CalendarPickerView.SelectionMode.Single)
                    .WithHighlightedDates(highlightedDates);
                _pickerView.DateSelected += (sender, args) =>
                {
                    element.NotifyDateSelected(args.Date);
                    highlightedDates.Add(args.Date);
                    HighlightedDatesChanged(highlightedDates);
                };
                
                AddView(_view);
            }
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);

            var msw = MeasureSpec.MakeMeasureSpec(r - l, MeasureSpecMode.Exactly);
            var msh = MeasureSpec.MakeMeasureSpec(b - t, MeasureSpecMode.Exactly);

            _view.Measure(msw, msh);
            _view.Layout(0, 0, r - l, b - t);
        }

        public void HighlightedDatesChanged(List<DateTime> highlightedDates)
        {
            _pickerView.HighlightDates(highlightedDates);
        }

    }
}