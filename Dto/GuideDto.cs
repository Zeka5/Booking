using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Dto
{
    public class GuideDto:INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int UserId {  get; set; }

        public bool IsSuperGuide { get; set; }

        public DateOnly SuperGuideStartDate { get; set; }

        private string firstName;
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                if (value != firstName)
                {
                    firstName = value;
                    OnPropertyChanged("FirstName");
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

        private string phoneNumber;
        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                if (value != phoneNumber)
                {
                    phoneNumber = value;
                    OnPropertyChanged("PhoneNumber");
                }

            }
        }

        private string biography;
        public string Biography
        {
            get
            {
                return biography;
            }
            set
            {
                if (value != biography)
                {
                    biography = value;
                    OnPropertyChanged("Biography");
                }

            }
        }

        private string status;
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                if (value != status)
                {
                    status = value;
                    OnPropertyChanged("Status");
                }

            }
        }

        public GuideDto() { }
        public GuideDto(Guide guide)
        {
            Id = guide.Id;
            UserId=guide.UserId;
            FirstName = guide.FirstName;
            LastName = guide.LastName;
            Email = guide.Email;
            PhoneNumber = guide.PhoneNumber;
            Biography = guide.Biography;
            IsSuperGuide =guide.IsSuperGuide;
            if (IsSuperGuide) Status = "Super Guide";
            else Status = "Guide";
            SuperGuideStartDate = guide.SuperGuideStartDate;
        }

        public Guide ToGuide()
        {
            return new Guide(Id,UserId,firstName,lastName,email,phoneNumber,biography,IsSuperGuide,SuperGuideStartDate);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
