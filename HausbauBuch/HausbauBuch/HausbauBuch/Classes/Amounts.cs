using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HausbauBuch.Classes
{
    public class Amounts : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _activitiesAmount;

        public int ActivitiesAmount
        {
            get { return _activitiesAmount; }
            set
            {
                if (value.Equals(_activitiesAmount))
                {
                    return;
                }
                _activitiesAmount = value;
                OnPropertyChanged();
            }
        }

        private int _appointmentsAmount;

        public int AppointmentsAmount
        {
            get { return _appointmentsAmount; }
            set
            {
                if (value.Equals(_appointmentsAmount))
                {
                    return;
                }
                _appointmentsAmount = value;
                OnPropertyChanged();
            }
        }

        private int _contactsAmount;

        public int ContactsAmount
        {
            get { return _contactsAmount; }
            set
            {
                if (value.Equals(_contactsAmount))
                {
                    return;
                }
                _contactsAmount = value;
                OnPropertyChanged();
            }
        }

        private int _documentsAmount;

        public int DocumentsAmount
        {
            get { return _documentsAmount; }
            set
            {
                if (value.Equals(_documentsAmount))
                {
                    return;
                }
                _documentsAmount = value;
                OnPropertyChanged();
            }
        }

        private int _enviromentsAmount;

        public int EnviromentsAmount
        {
            get { return _enviromentsAmount; }
            set
            {
                if (value.Equals(_enviromentsAmount))
                {
                    return;
                }
                _enviromentsAmount = value;
                OnPropertyChanged();
            }
        }

        private int _gardenAmount;

        public int GardenAmount
        {
            get { return _gardenAmount; }
            set
            {
                if (value.Equals(_gardenAmount))
                {
                    return;
                }
                _gardenAmount = value;
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}