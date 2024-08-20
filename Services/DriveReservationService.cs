using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.WPF.View.Guest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class DriveReservationService
    {
        private IDriveReservationRepository driveReservationRepository;
        private VehicleLocationService vehicleLocationService;
        private UserService userService;

        private int driverId, minUserDrives;

        public DriveReservationService(IDriveReservationRepository driveReservationRepository, VehicleLocationService vehicleLocationService, UserService userService)
        {
            this.driveReservationRepository = driveReservationRepository;
            this.vehicleLocationService = vehicleLocationService;
            this.userService = userService;
        }

        public DriveReservationService(IDriveReservationRepository driveReservationRepository)
        {
            this.driveReservationRepository = driveReservationRepository;
        }

        public void DismissBreakRequest(int driverId)
        {
            foreach (DriveReservation reservation in GetByOriginalDriverId(driverId))
            {
                if ((DateTime.Now - reservation.ReservationTime).TotalMinutes >= 3 && reservation.DriverId == 0)
                {
                    reservation.DriverId = reservation.OriginalDriverId;
                    reservation.OriginalDriverId = 0;
                    reservation.ReservationTime = DateTime.Now;
                    UpdateDriveReservation(reservation);
                }
            }
        }

        public List<DriveReservation> GetByOriginalDriverId(int driverId)
        {
            return driveReservationRepository.GetAllByOriginalDriverId(driverId);
        }

        public void MakeBreakRequest(int id, DateTime SelectedDateFrom, DateTime SelectedDateUntil)
        {
            foreach (DriveReservation reservation in GetOrderedReservationsById(id))
            {
                if (reservation.DepartureTime >= SelectedDateFrom && reservation.DepartureTime <= SelectedDateUntil)
                {
                    reservation.OriginalDriverId = reservation.DriverId;
                    reservation.DriverId = 0;
                    reservation.ReservationTime = DateTime.Now;
                    UpdateDriveReservation(reservation);
                }
            }
        }

        public void MakeUrgentBreakRequest(int id, DateTime SelectedDateFrom, DateTime SelectedDateUntil)
        {
            foreach(DriveReservation reservation in GetOrderedReservationsById(id))
            {
                if (reservation.DepartureTime >= SelectedDateFrom && reservation.DepartureTime <= SelectedDateUntil)
                {
                    driveReservationRepository.EditDriverId(reservation, ChooseAnotherDriver(id, reservation));
                }
            }
        }

        public int ChooseAnotherDriver(int id, DriveReservation reservation)
        {
            minUserDrives = int.MaxValue;
            driverId = 0;
            var otherUsers = userService.GetAllDrivers();
            otherUsers.Remove(userService.GetById(id));
            foreach (User user in otherUsers)
            {
                if (!vehicleLocationService.IsRegisteredForLocation(reservation, user.Id)) { continue; }
                CountUserDrives(user.Id);
            }

            return driverId;
        }

        public void Add(DriveReservation driveReservation)
        {
            driveReservationRepository.Add(driveReservation);
        }

        public void UpdateDriveReservation(DriveReservation driveReservation)
        {
            driveReservationRepository.Update(driveReservation);
        }

        public DriveReservation GetById(int id)
        {
            return driveReservationRepository.GetById(id);
        }

        public List<DriveReservation> GetOrderedReservationsById(int driverId)
        {
            return driveReservationRepository.GetAllById(driverId).OrderBy(x => x.DepartureTime).ToList();
        }

        public List<DriveReservation> LoadExtraDrives()
        {
            List<DriveReservation> extraDriveReservations = new List<DriveReservation>();
            foreach (DriveReservation driveReservation in driveReservationRepository.GetAll())
            {
                if (driveReservation.DriverId == 0)
                {
                    CheckForExtraDrives(driveReservation, extraDriveReservations);
                }
            }
            return extraDriveReservations;
        }

        public List<DriveReservation> LoadReservations(int driverId)
        {
            List<DriveReservation> driveReservations = new List<DriveReservation>();
            foreach (DriveReservation driveReservation in driveReservationRepository.GetAll())
            {
                if (driveReservation.DriverId == driverId)
                    driveReservations.Add(driveReservation);
            }
            return driveReservations;
        }

        private void CheckForExtraDrives(DriveReservation driveReservation, List<DriveReservation> extraDriveReservations)
        {
            TimeSpan reservationDuration = DateTime.Now - driveReservation.ReservationTime;
            if (reservationDuration > TimeSpan.FromMinutes(5))
            {
                driveReservationRepository.EditDriverId(driveReservation, GetLeastFrequentDriver(driveReservation));
            }
            extraDriveReservations.Add(driveReservation);
        }

        private int GetLeastFrequentDriver(DriveReservation driveReservation)
        {
            minUserDrives = int.MaxValue;
            driverId = 0;
            foreach (User user in userService.GetAllDrivers())
            {
                if (!vehicleLocationService.IsRegisteredForLocation(driveReservation, user.Id)) { continue; }
                CountUserDrives(user.Id);
            }

            return driverId;
        }

        private void CountUserDrives(int userId)
        {
            int count = 0;
            foreach (DriveReservation driveReservation in driveReservationRepository.GetTodayDrives())
            {
                if (userId == driveReservation.DriverId)
                    count++;
            }

            if (count < minUserDrives)
            {
                minUserDrives = count;
                driverId = userId;
            }
        }

        public void UpdateFastDrive(DriveReservation driveReservation, int driverId)
        {
            driveReservationRepository.EditDriverId(driveReservation, driverId);
        }

        public void Delete(DriveReservation driveReservation)
        {
            driveReservationRepository.Delete(driveReservation);
        }
        public List<DriveReservation> GetAll()
        {
            return driveReservationRepository.GetAll();
        }
        public DriveReservation Update(DriveReservation driveReservation)
        {
            return driveReservationRepository.Update(driveReservation);
        }
        public void Subscribe(IObserver observer)
        {
            driveReservationRepository.Subscribe(observer);
        }
    }
}
