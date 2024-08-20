using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class AddressService
    {
        private IAddressRepository addressRepository;

        public AddressService(IAddressRepository addressRepository)
        {
            this.addressRepository = addressRepository;
        }

        public Address GetById(int id)
        {
            return addressRepository.GetById(id);
        }

        public int GetLocationByReservation(DriveReservation driveReservation)
        {
            return addressRepository.GetLocationId(driveReservation.StartAddressId);
        }

        public int GetLocationId(int addressId)
        {
            return addressRepository.GetLocationId(addressId);
        }

        public string GetCurrentAddress(DriveReservation driveReservation)
        {
            Address startAddress = addressRepository.GetById(driveReservation.StartAddressId);
            Address endAddress = addressRepository.GetById(driveReservation.EndAddressId);
            string currentAddress = startAddress.Street + " " + startAddress.StreetNumber + " - " +
                                     endAddress.Street + " " + endAddress.StreetNumber;
            return currentAddress;
        }
    }
}
