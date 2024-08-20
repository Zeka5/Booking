using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Dto
{
    public class DriverDto : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }

            }
        }

        private string lastName;
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                if (value != lastName)
                {
                    lastName = value;
                    OnPropertyChanged("LastName");
                }

            }
        }

        private bool isSuperDriver;
        public bool IsSuperDriver
        {
            get
            {
                return isSuperDriver;
            }
            set
            {
                if (value != isSuperDriver)
                {
                    isSuperDriver = value;
                    OnPropertyChanged("IsSuperDriver");
                }

            }
        }

        private int points;
        public int Points
        {
            get
            {
                return points;
            }
            set
            {
                if (value != points)
                {
                    points = value;
                    OnPropertyChanged("Points");
                }

            }
        }

        private int age;
        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                if (value != age)
                {
                    age = value;
                    OnPropertyChanged("Age");
                }

            }
        }

        private string email;
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                if (value != email)
                {
                    email = value;
                    OnPropertyChanged("Email");
                }

            }
        }

        private string phone;
        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                if (value != phone)
                {
                    phone = value;
                    OnPropertyChanged("Phone");
                }

            }
        }

        private string membershipDate;
        public string MembershipDate
        {
            get
            {
                return membershipDate;
            }
            set
            {
                if (value != membershipDate)
                {
                    membershipDate = value;
                    OnPropertyChanged("MembershipDate");
                }

            }
        }
        
        public DriverDto()
        {
            
        }

        public DriverDto(Driver driver)
        {
            Id = driver.Id;
            Name = driver.Name;
            LastName = driver.LastName;
            IsSuperDriver = driver.IsSuperDriver;
            Points = driver.Points;
            Age = driver.Age;
            Email = driver.Email;
            Phone = driver.Phone;
            MembershipDate = driver.MembershipDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        }

        public Driver ToDriver()
        {
            return new Driver(Name, LastName, Age, Email, Phone, DateTime.ParseExact(MembershipDate,"dd/MM/yyyy", CultureInfo.InvariantCulture));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }

        }
    }
}
