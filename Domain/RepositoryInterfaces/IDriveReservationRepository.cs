using BookingApp.Domain.Model;
using BookingApp.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IDriveReservationRepository
    {
        List<DriveReservation> GetAll();
        List<DriveReservation> GetAllById(int driverId);
        List<DriveReservation> GetTodayDrives();
        int NextId();
        DriveReservation Add(DriveReservation driveReservation);
        DriveReservation Update(DriveReservation driveReservation);
        bool EditDriverId(DriveReservation driveReservation, int driverId);
        void Delete(DriveReservation driveReservation);
        void Subscribe(IObserver observer);
        List<DriveReservation> GetAllByOriginalDriverId(int id);
        DriveReservation? GetById(int id);
    }
}
