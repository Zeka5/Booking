using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.OwnerViewModels
{
    public class AccommodationViewModel : ViewModelBase
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }

        private string imagePath;
		public string ImagePath
		{
			get => imagePath;
			set
			{
				if (imagePath != value)
				{
					imagePath = value;
					OnPropertyChanged();
				}
			}
		}

        private string name;
        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }

        private string city;
        public string City
        {
            get => city;
            set
            {
                if (city != value)
                {
                    city = value;
                    OnPropertyChanged();
                }
            }
        }

        private string country;
        public string Country
        {
            get => country;
            set
            {
                if (country != value)
                {
                    country = value;
                    OnPropertyChanged();
                }
            }
        }

        private string type;
        public string Type
        {
            get => type;
            set
            {
                if (type != value)
                {
                    type = value;
                    OnPropertyChanged();
                }
            }
        }

        //RATING PROPERTY ZA PRIKAZ PROSECNOG RATINGA NA HOMEVIEW

        public AccommodationViewModel(int id, int ownerId, string imagePath, string name, string city, string country, string type)
        {
            Id = id;
            OwnerId = ownerId;
            this.imagePath = imagePath;
            this.name = name;
            this.city = city;
            this.country = country;
            this.type = type;
        }
    }
}
