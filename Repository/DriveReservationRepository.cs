using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Observer;
using BookingApp.Serializer;
using BookingApp.WPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class DriveReservationRepository : IDriveReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/driveReservation.csv";

        private readonly Serializer<DriveReservation> serializer;

        private List<DriveReservation> driveReservations;
        public Subject ReservationSubject;

        public DriveReservationRepository()
        {
            serializer = new Serializer<DriveReservation>();
            driveReservations = serializer.FromCSV(FilePath);
            ReservationSubject = new Subject();
        }

        public List<DriveReservation> GetAll()
        {
            return serializer.FromCSV(FilePath);
        }

        public List<DriveReservation> GetAllById(int id)
        {
            driveReservations = serializer.FromCSV(FilePath);
            return driveReservations.FindAll(d => d.DriverId == id);
        }

        public List<DriveReservation> GetAllByOriginalDriverId(int id)
        {
            driveReservations = serializer.FromCSV(FilePath);
            return driveReservations.FindAll(d => d.OriginalDriverId == id);
        }

        public List<DriveReservation> GetTodayDrives()
        {
            return driveReservations.FindAll(d => d.DepartureTime.Date == DateTime.Today.Date);
        }

        public int NextId()
        {
            driveReservations = serializer.FromCSV(FilePath);
            if (driveReservations.Count < 1)
            {
                return 1;
            }
            return driveReservations.Max(t => t.Id) + 1;
        }

        public DriveReservation Add(DriveReservation driveReservation)
        {
            driveReservations = serializer.FromCSV(FilePath);
            driveReservation.Id = NextId();
            driveReservations.Add(driveReservation);
            serializer.ToCSV(FilePath, driveReservations);
            ReservationSubject.NotifyObservers();
            return driveReservation;
        }

        public bool EditDriverId(DriveReservation driveReservation, int driverId)
        {
            if(driveReservation == null) { return false; }
            driveReservations.Find(d => d.Id == driveReservation.Id).DriverId = driverId;
            serializer.ToCSV(FilePath, driveReservations);
            ReservationSubject.NotifyObservers();
            return true;
        }

        public void Delete(DriveReservation driveReservation)
        {
            driveReservations = serializer.FromCSV(FilePath);
            DriveReservation founded = driveReservations.Find(t => t.Id == driveReservation.Id);
            driveReservations.Remove(founded);
            serializer.ToCSV(FilePath, driveReservations);
            ReservationSubject.NotifyObservers();
        }
        public DriveReservation Update(DriveReservation updatedReservation)
        {
            DriveReservation? oldReservation = GetById(updatedReservation.Id);
            if (oldReservation == null) { return null; }

            oldReservation.UserId = updatedReservation.UserId;
            oldReservation.DriverId = updatedReservation.DriverId;
            oldReservation.StartAddressId = updatedReservation.StartAddressId;
            oldReservation.EndAddressId = updatedReservation.EndAddressId;
            oldReservation.DepartureTime = updatedReservation.DepartureTime;
            oldReservation.IsFastReservation = updatedReservation.IsFastReservation;
            oldReservation.ReservationTime = updatedReservation.ReservationTime;
            oldReservation.IsTourGuestLate = updatedReservation.IsTourGuestLate;
            oldReservation.IsDriverLate = updatedReservation.IsDriverLate;
            oldReservation.OriginalDriverId = updatedReservation.OriginalDriverId;

            serializer.ToCSV(FilePath, driveReservations);
            return oldReservation;
        }

        public DriveReservation? GetById(int id)
        {
            return driveReservations.Find(x => x.Id == id);
        }

        public void Subscribe(IObserver observer)
        {
            ReservationSubject.Subscribe(observer);
        }

    }
}
