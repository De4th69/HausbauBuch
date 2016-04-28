using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HausbauBuch.Classes
{
    public class EntityLists : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        private ObservableCollection<Activities> _activityItems;

        public ObservableCollection<Activities> ActivityItems
        {
            get { return _activityItems; }
            set
            {
                if (value.Equals(_activityItems))
                {
                    return;
                }
                _activityItems = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Appointments> _appointmentItems;

        public ObservableCollection<Appointments> AppointmentItems
        {
            get { return _appointmentItems; }
            set
            {
                if (value.Equals(_appointmentItems))
                {
                    return;
                }
                _appointmentItems = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Contacts> _contactItems;

        public ObservableCollection<Contacts> ContactItems
        {
            get {return _contactItems; }
            set
            {
                if (value.Equals(_contactItems))
                {
                    return;
                }
                _contactItems = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Documents> _documentItems;

        public ObservableCollection<Documents> DocumentItems
        {
            get { return _documentItems; }
            set
            {
                if (value.Equals(_documentItems))
                {
                    return;
                }
                _documentItems = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Enviroments> _enviromentItems;

        public ObservableCollection<Enviroments> EnviromentItems
        {
            get {return _enviromentItems; }
            set
            {
                if (value.Equals(_enviromentItems))
                {
                    return;
                }
                _enviromentItems = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<GardenIdeas> _gardenItems;

        public ObservableCollection<GardenIdeas> GardenItems
        {
            get { return _gardenItems; }
            set
            {
                if (value.Equals(_gardenItems))
                {
                    return;
                }
                _gardenItems = value;
                OnPropertyChanged();
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
