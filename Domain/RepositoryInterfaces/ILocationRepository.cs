using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ILocationRepository
    {
        List<Location> GetAll();
        Location? GetById(int id);
        Location? GetByName(string country, string city);
    }
}
