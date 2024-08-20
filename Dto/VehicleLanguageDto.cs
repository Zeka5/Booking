using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Dto
{
    public class VehicleLanguageDto : INotifyPropertyChanged
    {
        private int vehicleId;
        public int VehicleId
        {
            get { return vehicleId; }
            set
            {
                if (vehicleId != value)
                {
                    vehicleId = value;
                    OnPropertyChanged("VehicleId");
                }
            }
        }

        private int languageId;
        public int LanguageId
        {
            get { return languageId; }
            set
            {
                if (languageId != value)
                {
                    languageId = value;
                    OnPropertyChanged("LanguageId");
                }
            }
        }

        public VehicleLanguageDto(VehicleLanguage vehicleLanguage)
        {
            vehicleId = vehicleLanguage.VehicleId;
            languageId = vehicleLanguage.LanguageId;
        }

        public VehicleLanguageDto() { }

        public VehicleLanguage ToVehicleLocations()
        {
            return new VehicleLanguage(VehicleId, LanguageId);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
