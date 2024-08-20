using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class UserService
    {
        private IUserRepository repository;

        public UserService(IUserRepository userRepository)
        {
            repository = userRepository;
        }

        public User GetById(int id)
        {
            return repository.GetById(id);
        }

        public string GetUsernameById(int id)
        {
            return repository.GetById(id).Username;
        }
        
        public List<User> GetAllDrivers()
        {
            return repository.GetAllByVocation(4);
        }
    }
}
