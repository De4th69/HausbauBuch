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

        private string _companyName;

        public string CompanyName
        {
            get { return _companyName; }
            set
            {
                if (value.Equals(_companyName))
                {
                    return;
                }
                _companyName = value;
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

        private string _mobileNumber;

        public string MobileNumber
        {
            get { return _mobileNumber; }
            set
            {
                if (value.Equals(_mobileNumber))
                {
                    return;
                }
                _mobileNumber = value;
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

        private string _internetAddress;

        public string InternetAddress
        {
            get { return _internetAddress; }
            set
            {
                if (value.Equals(_internetAddress))
                {
                    return;
                }
                _internetAddress = value;
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
            FullName = "";
            CompanyName = "";
            PhoneNumber = "";
            MobileNumber = "";
            Email = "";
            InternetAddress = "";
            ImagePath = "";
        }
    }
}