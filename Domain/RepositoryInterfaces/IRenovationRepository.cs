using BookingApp.Domain.Model;
using BookingApp.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IRenovationRepository
    {
        void Add(Renovation renovation);
        void Delete(int id);
        Renovation GetById(int id);
        List<Renovation> GetAll();
        void Subscribe(IObserver observer);
    }
}
