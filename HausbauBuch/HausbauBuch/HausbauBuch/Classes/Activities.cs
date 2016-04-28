using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace HausbauBuch.Classes
{
    public class Activities : Entity
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
                if (string.IsNullOrEmpty(value))
                {
                    _description = "";
                }
                _description = value;
                OnPropertyChanged();
            }
        }

        private DateTime _date;

        public DateTime Date
        {
            get {return _date;}
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

        private bool _finished;

        public bool Finished
        {
            get {return _finished; }
            set
            {
                if (value.Equals(_finished))
                {
                    return;
                }
                _finished = value;
                OnPropertyChanged();
            }
        }

        private bool _isCheckList;

        public bool IsCheckList
        {
            get { return _isCheckList; }
            set
            {
                if (value.Equals(_isCheckList))
                {
                    return;
                }
                _isCheckList = value;
                OnPropertyChanged();
            }
        }

        public Activities()
        {
            Title = "";
            Description = "";
            Date = DateTime.Now;
            Finished = false;
            IsCheckList = false;
            ModifiedAt = DateTime.Now;
            CreatedAt = DateTime.Now;
        }
    }
}
