using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GymMembershipApp.Models
{
    public class Member : INotifyPropertyChanged
    {
        private int _memberID;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _phoneNumber;
        private string _address;
        private DateTime _dateOfBirth;
        private string _membershipType;
        private DateTime _joinDate;
        private DateTime _expiryDate;
        private bool _activeStatus;

        public int MemberID
        {
            get => _memberID;
            set
            {
                if (_memberID != value)
                {
                    _memberID = value;
                    OnPropertyChanged();
                }
            }
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Address
        {
            get => _address;
            set
            {
                if (_address != value)
                {
                    _address = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                if (_dateOfBirth != value)
                {
                    _dateOfBirth = value;
                    OnPropertyChanged();
                }
            }
        }

        public string MembershipType
        {
            get => _membershipType;
            set
            {
                if (_membershipType != value)
                {
                    _membershipType = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime JoinDate
        {
            get => _joinDate;
            set
            {
                if (_joinDate != value)
                {
                    _joinDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime ExpiryDate
        {
            get => _expiryDate;
            set
            {
                if (_expiryDate != value)
                {
                    _expiryDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool ActiveStatus
        {
            get => _activeStatus;
            set
            {
                if (_activeStatus != value)
                {
                    _activeStatus = value;
                    OnPropertyChanged();
                }
            }
        }

        // Display property for list view
        public string FullName => $"{FirstName} {LastName}";

        public string Status => ActiveStatus ? "Active" : "Inactive";

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}