using BookingApp.Domain.Model;
using BookingApp.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IDriveRepository
    {
        List<Drive> GetAll();
        Drive GetById(int id);
        List<Drive> GetByMonthAndYear(int driverId, int month, int year);
        List<Drive> GetByDriverAndYear(int driverId, int year);
        int NextId();
        Drive Add(Drive drive);
        void Delete(Drive drive);
        void Subscribe(IObserver observer);
    }
}
