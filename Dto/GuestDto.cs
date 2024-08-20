using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.Dto
{
    public class GuestDto : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string name;
        public string Username
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
                    OnPropertyChanged();
                }

            }
        }
        private int bonusPoints;
        public int BonusPoints
        {
            get
            {
                return bonusPoints;
            }
            set
            {
                if (value != bonusPoints)
                {
                    bonusPoints = value;
                    OnPropertyChanged();
                }

            }
        }
        private Mode mode;
        public Mode Mode
        {
            get
            {
                return mode;
            }
            set
            {
                if (value != mode)
                {
                    mode = value;
                    OnPropertyChanged();
                }

            }
        }
        private DateTime dateTime;
        public DateTime SuperGuestConfigured
        {
            get
            {
                return dateTime;
            }
            set
            {
                if (value != dateTime)
                {
                    dateTime = value;
                    OnPropertyChanged();
                }

            }
        }
        public GuestDto() { }
        public GuestDto(Guest guest) {
            Id = guest.Id;
            Username = guest.Username;
            BonusPoints = guest.BonusPoints;
            Mode = guest.Mode;
            SuperGuestConfigured = guest.SuperGuestConfigured;
        }
        public Guest ToGuest() {
            return new Guest(Username,Mode,BonusPoints,SuperGuestConfigured);
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
