using System;

namespace HausbauBuch.Classes
{
    public class Appointments : Entity
    {
        private string _title;

        public string Title
        {
            get { return _title; }
            set
            {
                if (value.Equals(_title))
                {
                    return;
                }
                _title = value;
                OnPropertyChanged();
            }
        }

        private string _detail;

        public string Detail
        {
            get {  return _detail; }
            set
            {
                if (value.Equals(_detail))
                {
                    return;
                }
                _detail = value;
                OnPropertyChanged();
            }
        }

        private string _place;

        public string Place
        {
            get { return _place; }
            set
            {
                if (value.Equals(_place))
                {
                    return;
                }
                _place = value;
                OnPropertyChanged();
            }
        }
        
        private DateTime _startTime;

        public DateTime StartTime
        {
            get { return _startTime; }
            set
            {
                if (value.Equals(_startTime))
                {
                    return;
                }
                _startTime = value;
                OnPropertyChanged();
            }
        }

        private DateTime _endTime;

        public DateTime EndTime
        {
            get { return _endTime; }
            set
            {
                if (value.Equals(_endTime))
                {
                    return;
                }
                _endTime = value;
                OnPropertyChanged();
            }
        }

        public Appointments()
        {
            Title = "";
            Detail = "";
            Place = "";
            StartTime = DateTime.Now;
            EndTime = DateTime.Now.AddHours(1);
        }
    }
}
