using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Observer;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class CheckPointRepository:ICheckPointRepository
    {
        private const string FilePath = "../../../Resources/Data/checkpoints.csv";

        private readonly Serializer<CheckPoint> serializer;

        private List<CheckPoint> checkPoints;

        public Subject CheckPointSubject;


        public CheckPointRepository()
        {
            serializer = new Serializer<CheckPoint>();
            checkPoints = serializer.FromCSV(FilePath);
            CheckPointSubject = new Subject();
        }
        public List<CheckPoint> GetAll()
        {
            return serializer.FromCSV(FilePath);
        }
        public CheckPoint GetById(int id)
        {
            checkPoints = serializer.FromCSV(FilePath);
            return checkPoints.Find(ch=>ch.Id==id);
        }

        public int NextId()
        {
            checkPoints = serializer.FromCSV(FilePath);
            if (checkPoints.Count < 1)
            {
                return 1;
            }
            return checkPoints.Max(ch => ch.Id) + 1;
        }

        public CheckPoint Add(CheckPoint checkPoint)
        {
            checkPoints = serializer.FromCSV(FilePath);
            checkPoint.Id = NextId();
            checkPoints.Add(checkPoint);
            serializer.ToCSV(FilePath, checkPoints);
            CheckPointSubject.NotifyObservers();
            return checkPoint;
        }

        public CheckPoint Update(CheckPoint checkPoint)
        {
            checkPoints = serializer.FromCSV(FilePath);
            CheckPoint current = checkPoints.Find(ch => ch.Id == ch.Id);
            int index = checkPoints.IndexOf(current);
            checkPoints.Remove(current);
            checkPoints.Insert(index, checkPoint);       // keep ascending order of ids in file 
            serializer.ToCSV(FilePath, checkPoints);
            CheckPointSubject.NotifyObservers();
            return checkPoint;
        }

        public void Delete(CheckPoint checkPoint)
        {
            checkPoints = serializer.FromCSV(FilePath);
            CheckPoint founded = checkPoints.Find(ch => ch.Id == checkPoint.Id);
            checkPoints.Remove(founded);
            serializer.ToCSV(FilePath, checkPoints);
            CheckPointSubject.NotifyObservers();
        }

        public void Subscribe(IObserver observer)
        {
            CheckPointSubject.Subscribe(observer);
        }
    }
}
