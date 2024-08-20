using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IGuestRepository
    {
        void Add(Guest guest);
        List<Guest> GetAll();
        Guest GetById(int id);
        void Update(Guest guest);
    }
}
