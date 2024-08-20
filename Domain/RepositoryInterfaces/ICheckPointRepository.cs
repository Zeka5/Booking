using BookingApp.Domain.Model;
using BookingApp.Observer;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ICheckPointRepository
    {
        List<CheckPoint> GetAll();
        CheckPoint GetById(int id);
        int NextId();
        CheckPoint Add(CheckPoint checkPoint);
        CheckPoint Update(CheckPoint checkPoint);
        void Delete(CheckPoint checkPoint);
        void Subscribe(IObserver observer);
    }
}
