using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private DateTime _date;

        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (value.Equals(_date))
                {
                    return;
                }
                _date = value;
                OnPropertyChanged();
            }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set
            {
                if (value.Equals(_description))
                {
                    return;
                }
                _description = value;
                OnPropertyChanged();
            }
        }

        private List<Contacts> _contacts;

        public List<Contacts> Contacts
        {
            get { return _contacts; }
            set
            {
                if (value.Equals(_contacts))
                {
                    return;
                }
                _contacts = value;
                OnPropertyChanged();
            }
        }

        private string _location;

        public string Location
        {
            get { return _location; }
            set
            {
                if (value.Equals(_location))
                {
                    return;
                }
                _location = value;
                OnPropertyChanged();
            }
        }

        public Appointments()
        {
            Title = "";
            Location = "";
            Description = "";
            Contacts = new List<Contacts>();
            Date = DateTime.Now;
            StartTime = DateTime.Now;
            EndTime = DateTime.Now.AddHours(1);
        }
    }
}
