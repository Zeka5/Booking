using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IUserRepository
    {
        User GetByUsername(string username);
        User GetById(int id);
        List<User> GetAllByVocation(int vocation);
    }
}
