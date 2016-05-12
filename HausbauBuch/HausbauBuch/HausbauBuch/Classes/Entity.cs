using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace HausbauBuch.Classes
{
    public class Entity : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        [PrimaryKey, AutoIncrement]
        public int? Id { get; set; }

        private DateTime _createdAt;

        public DateTime CreatedAt
        {
            get { return _createdAt; }
            set
            {
                if (value.Equals(_createdAt))
                {
                    return;
                }
                _createdAt = value;
                OnPropertyChanged();
            }
        }

        private DateTime _modifiedAt;

        public DateTime ModifiedAt
        {
            get {return _modifiedAt; }
            set
            {
                if (value.Equals(_modifiedAt))
                {
                    return;
                }
                _modifiedAt = value;
                OnPropertyChanged();
            }
        }

        private DateTime _finishedOn;

        public DateTime FinishedOn
        {
            get {return _finishedOn; }
            set
            {
                if (value.Equals(_finishedOn))
                {
                    return;
                }
                _finishedOn = value;
                OnPropertyChanged();
            }
        }

        private bool _deleted;

        public bool Deleted
        {
            get { return _deleted; }
            set
            {
                if (value.Equals(_deleted))
                {
                    return;
                }
                _deleted = value;
                OnPropertyChanged();
            }
        }
        
        private bool _synced;

        public bool Synced
        {
            get { return _synced; }
            set
            {
                if (value.Equals(_synced))
                {
                    return;
                }
                _synced = value;
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

        public Entity()
        {
            CreatedAt = DateTime.Now;
            ModifiedAt = DateTime.Now;
        }
    }
}
