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
    public class DriveRepository : IDriveRepository
    {
        private const string FilePath = "../../../Resources/Data/drives.csv";

        private readonly Serializer<Drive> serializer;

        private List<Drive> drives;
        public Subject DriveSubject;

        public DriveRepository()
        {
            DriveSubject = new Subject();
            serializer = new Serializer<Drive>();
            drives = serializer.FromCSV(FilePath);
        }

        public List<Drive> GetAll()
        {
            return serializer.FromCSV(FilePath);
        }

        public Drive GetById(int id)
        {
            drives = serializer.FromCSV(FilePath);
            return drives.Find(d => d.Id == id);
        }

        public List<Drive> GetByMonthAndYear(int driverId, int month, int year)
        {
            drives = serializer.FromCSV(FilePath);
            return drives.FindAll(d => d.DriverId == driverId && d.EndTime.Month == month && d.EndTime.Year == year);
        }

        public List<Drive> GetByDriverAndYear(int driverId, int year)
        {
            drives = serializer.FromCSV(FilePath);
            return drives.FindAll(d => d.DriverId == driverId && d.EndTime.Year == year);
        }

        public int NextId()
        {
            drives = serializer.FromCSV(FilePath);
            if (drives.Count < 1)
            {
                return 1;
            }
            return drives.Max(d => d.Id) + 1;
        }

        public Drive Add(Drive drive)
        {
            drives = serializer.FromCSV(FilePath);
            drive.Id = NextId();
            drives.Add(drive);
            serializer.ToCSV(FilePath, drives);
            return drive;
        }

        public void Delete(Drive drive)
        {
            drives = serializer.FromCSV(FilePath);
            Drive founded = drives.Find(d => d.Id == drive.Id);
            drives.Remove(founded);
            serializer.ToCSV(FilePath, drives);
        }

        public void Subscribe(IObserver observer)
        {
            DriveSubject.Subscribe(observer);
        }
    }
}
