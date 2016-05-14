using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HausbauBuch.Controls
{
    public class CalendarView : View
    {
        public static readonly BindableProperty HighlightedDaysProperty = BindableProperty.Create("HighlightedDays", typeof(List<DateTime>), typeof(CalendarView));

        public List<DateTime> HighlightedDays
        {
            get { return (List<DateTime>)GetValue(HighlightedDaysProperty);}
            set { SetValue(HighlightedDaysProperty, value); }
        }

        public static readonly BindableProperty SelectedDateProperty = BindableProperty.Create("SelectedDate", typeof(DateTime), typeof(CalendarView), DateTime.Now);

        public DateTime SelectedDate
        {
            get { return (DateTime) GetValue(SelectedDateProperty); }
            set { SetValue(SelectedDateProperty, value);}
        }

        public void NotifyDateSelected(DateTime dateSelected)
        {
            DateSelected?.Invoke(this, dateSelected);
        }

        public event EventHandler<DateTime> DateSelected;
    }
}
