using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IDriverRepository
    {
        List<Driver> GetAll();
        Driver? GetById(int id);
        Driver? EditSuperDriver(Driver driver);
        Driver? EditDriver(Driver driver);
        Driver Update(Driver driver);
    }
}
