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

        private int _size;

        public int Size
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

        private string _documentPath;

        public string DocumentPath
        {
            get { return _documentPath; }
            set
            {
                if (value.Equals(_documentPath))
                {
                    return;
                }
                _documentPath = value;
                OnPropertyChanged();
            }
        }
    }
}
