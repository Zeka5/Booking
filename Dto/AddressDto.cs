using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Dto
{
    public class AddressDto : INotifyPropertyChanged
    {
        public int AdressId { get; set; }
        public int LocationId { get; set; }

        private string street;
        public string Street
        {
            get { return street; }
            set
            {
                if (value != street)
                {
                    street = value;
                    OnPropertyChanged("Street");
                }
            }
        }

        private string streetNumber;
        public string StreetNumber
        {
            get { return streetNumber; }
            set
            {
                if (value != streetNumber)
                {
                    streetNumber = value;
                    OnPropertyChanged("StreetNumber");
                }
            }
        }

        public AddressDto() { }

        public AddressDto(Address adress)
        {
            AdressId = adress.AdressId;
            LocationId = adress.Id;
            Street = adress.Street;
            StreetNumber = adress.StreetNumber.ToString();
        }

        public Address ToAdress()
        {
            return new Address(Street, int.Parse(StreetNumber), LocationId);
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
