using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Dto
{
    public class VehicleLocationsDto : INotifyPropertyChanged
    {
        private int vehicleId;
        public int VehicleId
        {
            get { return vehicleId; }
            set
            {
                if(vehicleId != value)
                {
                    vehicleId = value;
                    OnPropertyChanged("VehicleId");
                }
            }
        }

        private int locationId;
        public int LocationId
        {
            get { return locationId; }
            set
            {
                if(locationId != value)
                {
                    locationId = value;
                    OnPropertyChanged("LocationId");
                }
            }
        }

        public VehicleLocationsDto(VehicleLocations vehicleLocation)
        {
            vehicleId = vehicleLocation.VehicleId;
            locationId = vehicleLocation.LocationId;
        }

        public VehicleLocationsDto() { }

        public VehicleLocations ToVehicleLocations()
        {
            return new VehicleLocations(VehicleId, LocationId);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
