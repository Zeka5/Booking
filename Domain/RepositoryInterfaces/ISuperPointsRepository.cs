using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ISuperPointsRepository
    {
        List<SuperPoints> GetAll();
        List<SuperPoints> GetAllByUserId(int id);
        int NextId();
        void Add(int userId, int value);
        void DeleteAllByUserId(int userId);
    }
}
