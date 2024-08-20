using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class SuperPointsRepository : ISuperPointsRepository
    {
        private const string filePath = "../../../Resources/Data/superpoints.csv";
        private readonly Serializer<SuperPoints> serializer;
        private List<SuperPoints> points;

        public SuperPointsRepository()
        {
            this.serializer = new Serializer<SuperPoints>();
            this.points = this.serializer.FromCSV(filePath);
        }

        public void Add(int userId, int value)
        {
            points = serializer.FromCSV(filePath);
            SuperPoints superPoints = new SuperPoints(userId, value);
            superPoints.Id = NextId();
            points.Add(superPoints);
            serializer.ToCSV(filePath, points);
        }

        public void DeleteAllByUserId(int userId)
        {
            points = serializer.FromCSV(filePath);
            List<SuperPoints> foundPoints = points.FindAll(sp => sp.UserId == userId);
            foreach (SuperPoints point in foundPoints)
            {
                points.Remove(point);
            }
            serializer.ToCSV(filePath, points);
        }

        public List<SuperPoints> GetAll()
        {
            return this.points;
        }

        public List<SuperPoints> GetAllByUserId(int id)
        {
            return points.FindAll(sp => sp.UserId == id);
        }

        public int NextId()
        {
            points = serializer.FromCSV(filePath);
            if (points.Count < 1)
            {
                return 1;
            }
            return points.Max(t => t.Id) + 1;
        }
    }
}
