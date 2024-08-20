using BookingApp.Domain.Model;
using BookingApp.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IAddressRepository
    {
        List<Address> GetAll();
        Address GetById(int id);
        int GetLocationId(int id);
        int NextId();
        Address Add(Address address);
        void Delete(Address address);
        Address GetLast();
        Address GetBeforeLast();
        Address Update(Address address);
        void Subscribe(IObserver observer);
    }
}
