using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HausbauBuch.Classes
{
    public class Contacts : Entity
    {
        private string _firstName;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (value.Equals(_firstName))
                {
                    return;
                }
                _firstName = value;
                OnPropertyChanged();
            }
        }

        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (value.Equals(_lastName))
                {
                    return;
                }
                _lastName = value;
                OnPropertyChanged();
            }
        }

        private string _fullName;

        public string FullName
        {
            get { return _fullName; }
            set
            {
                if (value.Equals(_fullName))
                {
                    return;
                }
                _fullName = value;
                OnPropertyChanged();
            }
        }

        private string _phoneNumber;

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                if (value.Equals(_phoneNumber))
                {
                    return;
                }
                _phoneNumber = value;
                OnPropertyChanged();
            }
        }

        private string _mobile;

        public string Mobile
        {
            get { return _mobile; }
            set
            {
                if (value.Equals(_mobile))
                {
                    return;
                }
                _mobile = value;
                OnPropertyChanged();
            }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set
            {
                if (value.Equals(_email))
                {
                    return;
                }
                _email = value;
                OnPropertyChanged();
            }
        }

        private string _website;

        public string Website
        {
            get { return _website; }
            set
            {
                if (value.Equals(_website))
                {
                    return;
                }
                _website = value;
                OnPropertyChanged();
            }
        }

        private string _imagePath;

        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                if (value.Equals(_imagePath))
                {
                    return;
                }
                _imagePath = value;
                OnPropertyChanged();
            }
        }

        public Contacts()
        {
            FirstName = "";
            LastName = "";
            PhoneNumber = "";
            Mobile = "";
            Website = "";
            ImagePath = "";
        }
    }
}
