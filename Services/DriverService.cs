using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class DriverService
    {
        private IDriverRepository driverRepository;

        public DriverService(IDriverRepository driverRepository)
        {
            this.driverRepository = driverRepository;
        }

        public Driver EditSuperDriver(Driver driver)
        {
            return driverRepository.EditSuperDriver(driver);
        }

        public Driver EditDriver(Driver driver)
        {
            return driverRepository.EditDriver(driver);
        }

        public Driver GetByUserId(int id)
        {
            return driverRepository.GetById(id);
        }
        public Driver Update(Driver driver)
        {
            return driverRepository.Update(driver);
        }
    }
}
