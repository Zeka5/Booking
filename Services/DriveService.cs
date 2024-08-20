using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class DriveService
    {
        private IDriveRepository driveRepository;
        private AddressService addressService;
        private LocationService locationService;
        public DriveService(IDriveRepository driveRepository)
        {
            this.driveRepository = driveRepository;
        }

        public DriveService(IDriveRepository driveRepository, AddressService addressService, LocationService locationService)
        {
            this.driveRepository = driveRepository;
            this.addressService = addressService;
            this.locationService = locationService;
        }

        public int GetMostFrequentLocation()
        {
            List<int> locations = new List<int>();
            foreach(Drive drive in driveRepository.GetAll())
            {
                locations.Add(addressService.GetLocationId(drive.StartAddressId));
            }

            return GetMostFrequentId(locations);
        }

        private int GetMostFrequentId(List<int> ids)
        {
            Dictionary<int, int> idCounts = new Dictionary<int, int>();

            foreach (int id in ids)
            {
                if (idCounts.ContainsKey(id))
                {
                    idCounts[id]++;
                }
                else
                {
                    idCounts[id] = 1;
                }
            }
            int mostFrequentId = idCounts.OrderByDescending(x => x.Value).First().Key;

            return mostFrequentId;
        }

        public List<int> GetLeastFrequentLocations()
        {
            List<int> locations = new List<int>();
            foreach (Drive drive in driveRepository.GetAll())
            {
                locations.Add(addressService.GetLocationId(drive.StartAddressId));
            }

            return GetLeastFrequentId(locations);
        }

        private List<int> GetLeastFrequentId(List<int> ids)
        {
            List<int> leastFrequent = new List<int>();
            Dictionary<int, int> idCounts = new Dictionary<int, int>();
            //ako se nijedna voznja ne odvija na toj lokaciji
            foreach (Location location in locationService.GetAll())
            {
                if (!ids.Contains(location.Id))
                {
                    leastFrequent.Add(location.Id);
                }
            }
            //ili ako je zapravo najmanja potraznja za tom lokacijom
            foreach (int id in ids)
            {
                if (idCounts.ContainsKey(id))
                {
                    idCounts[id]++;
                }
                else
                {
                    idCounts[id] = 1;
                }
            }
            int leastFrequentId = idCounts.OrderByDescending(x => x.Value).Last().Key;
            leastFrequent.Add(leastFrequentId);

            return leastFrequent;
        }

        public void Add(Drive drive)
        {
            driveRepository.Add(drive);
        }

        public void Subscribe(IObserver observer)
        {
            driveRepository.Subscribe(observer);
        }

        public List<Drive> GetByDriverAndYear(int driverId, int month)
        {
            return driveRepository.GetByDriverAndYear(driverId, month);
        }

        public int GetMinYear()
        {
            return driveRepository.GetAll().Min(d => d.EndTime.Year);
        }
    }
}
