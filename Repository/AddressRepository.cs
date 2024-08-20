using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Observer;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private const string FilePath = "../../../Resources/Data/address.csv";

        private readonly Serializer<Address> serializer;

        private List<Address> adress;

        public Subject adressSubject;

        public AddressRepository()
        {
            serializer = new Serializer<Address>();
            adress = serializer.FromCSV(FilePath);
            adressSubject = new Subject();
        }
        public List<Address> GetAll()
        {
            return serializer.FromCSV(FilePath);
        }
        public Address GetById(int id)
        {
            adress = serializer.FromCSV(FilePath);
            return adress.Find(a => a.AdressId == id);
        }

        public int GetLocationId(int id)
        {
            return GetById(id).Id;
        }

        public int NextId()
        {
            adress = serializer.FromCSV(FilePath);
            if (adress.Count < 1)
            {
                return 1;
            }
            return adress.Max(a => a.AdressId) + 1;
        }

        public Address Add(Address adress)
        {
            this.adress = serializer.FromCSV(FilePath);
            adress.AdressId = NextId();
            this.adress.Add(adress);
            serializer.ToCSV(FilePath, this.adress);
            adressSubject.NotifyObservers();
            return adress;
        }

        public void Delete(Address adress)
        {
            this.adress = serializer.FromCSV(FilePath);
            Address founded = this.adress.Find(a => a.AdressId == adress.AdressId);
            this.adress.Remove(founded);
            serializer.ToCSV(FilePath, this.adress);
            adressSubject.NotifyObservers();
        }

        public Address GetLast()
        {
            return adress[^1];
        }
        public Address GetBeforeLast()
        {
            return adress[^2];
        }
        public Address Update(Address adress)
        {
            this.adress = serializer.FromCSV(FilePath);
            Address current = this.adress.Find(a => a.AdressId == adress.AdressId);
            int index = this.adress.IndexOf(current);
            this.adress.Remove(current);
            this.adress.Insert(index, adress);       // keep ascending order of ids in file 
            serializer.ToCSV(FilePath, this.adress);
            adressSubject.NotifyObservers();
            return adress;
        }
        public void Subscribe(IObserver observer)
        {
            adressSubject.Subscribe(observer);
        }
    }
}

