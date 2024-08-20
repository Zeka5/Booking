using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private const string FilePath = "../../../Resources/Data/users.csv";

        private readonly Serializer<User> serializer;

        private List<User> users;

        public UserRepository()
        {
            serializer = new Serializer<User>();
            users = serializer.FromCSV(FilePath);
        }

        public User GetByUsername(string username)
        {
            users = serializer.FromCSV(FilePath);
            return users.FirstOrDefault(u => u.Username == username);
        }

        public List<User> GetAllByVocation(int vocation)
        {
            return users.FindAll(u => u.Vocation == vocation);
        }

        public User GetById(int id)
        {
            return users.FirstOrDefault(x => x.Id == id);
        }
    }
}
