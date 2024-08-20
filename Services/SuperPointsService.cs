using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class SuperPointsService
    {
        private ISuperPointsRepository superPointsRepository;
        public SuperPointsService(ISuperPointsRepository superPointsRepository)
        {
            this.superPointsRepository = superPointsRepository;
        }

        public List<SuperPoints> GetAllByUserId(int userId)
        {
            return superPointsRepository.GetAllByUserId(userId);
        }

        public void AddPoint(int userId, int value)
        {
            superPointsRepository.Add(userId, value);
        }

        public void DeleteAllPoints(int userId)
        {
            superPointsRepository.DeleteAllByUserId(userId);
        }
    }
}
