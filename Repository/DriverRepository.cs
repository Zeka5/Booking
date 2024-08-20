using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using BookingApp.WPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class DriverRepository : IDriverRepository
    {
        private const string filePath = "../../../Resources/Data/drivers.csv";
        private readonly Serializer<Driver> serializer;
        private List<Driver> drivers;

        public DriverRepository()
        {
            this.serializer = new Serializer<Driver>();
            this.drivers = this.serializer.FromCSV(filePath);
        }

        public List<Driver> GetAll()
        {
            return this.drivers;
        }

        public Driver? EditSuperDriver(Driver driver)
        {
            Driver? oldDriver = GetById(driver.Id);
            if (oldDriver == null) { return null; }

            oldDriver.IsSuperDriver = driver.IsSuperDriver;
            oldDriver.MembershipDate = driver.MembershipDate;
            oldDriver.Points = driver.Points;

            serializer.ToCSV(filePath, drivers);
            return oldDriver;
        }

        public Driver? EditDriver(Driver driver)
        {
            Driver? oldDriver = GetById(driver.Id);
            if (oldDriver == null){return null;}

            oldDriver.Name = driver.Name;
            oldDriver.LastName = driver.LastName;
            oldDriver.Age = driver.Age;
            oldDriver.Email = driver.Email;
            oldDriver.Phone = driver.Phone;
            oldDriver.IsFirstLogIn = driver.IsFirstLogIn;

            serializer.ToCSV(filePath, drivers);
            return oldDriver;
        }
        public Driver? GetById(int id)
        {
            return drivers.Find(driver => driver.Id == id);
        }
        public Driver Update(Driver driver)
        {
            drivers = serializer.FromCSV(filePath);
            Driver current = drivers.Find(t => t.Id == driver.Id);
            int index = drivers.IndexOf(current);
            drivers.Remove(current);
            drivers.Insert(index, driver);       // keep ascending order of ids in file 
            serializer.ToCSV(filePath, drivers);
            return driver;
        }
    }
}
