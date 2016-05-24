using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HausbauBuch.Classes
{
    public class Documents : Entity
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                if (value.Equals(_name))
                {
                    return;
                }
                _name = value;
                OnPropertyChanged();
            }
        }

        private long _size;

        public long Size
        {
            get { return _size; }
            set
            {
                if (value.Equals(_size))
                {
                    return;
                }
                _size = value;
                OnPropertyChanged();
            }
        }

        private string _documentType;

        public string DocumentType
        {
            get { return _documentType; }
            set
            {
                if (value.Equals(_documentType))
                {
                    return;
                }
                _documentType = value;
                OnPropertyChanged();
            }
        }

        public Documents()
        {
            Name = "";
            Size = 0;
            DocumentType = "";
        }
    }
}
