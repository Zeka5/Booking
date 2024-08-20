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
    public class VehicleDto : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private int capacity;

        public int Capacity
        {
            get { return capacity; }
            set
            {
                if (value != capacity)
                {
                    capacity = value;
                    OnPropertyChanged("Capacity");
                }
            }
        }


        private int driverId;
        public int DriverId
        {
            get { return driverId; }
            set
            {
                if(driverId != value)
                {
                    driverId = value;
                    OnPropertyChanged("DriverId");
                }
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if(name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public VehicleDto() { }

        public VehicleDto(Vehicle vehicle)
        {
            Id = vehicle.Id;
            capacity = vehicle.Capacity;
            driverId = vehicle.DriverId;
            name = vehicle.Name;
        }

        public Vehicle ToVehicle()
        {
            return new Vehicle(Capacity, DriverId, Name);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public virtual void OnPropertyChanged(string name)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
