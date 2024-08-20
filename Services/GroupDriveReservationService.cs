using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class GroupDriveReservationService
    {
        private IGroupDriveReservationRepository groupReservationRepository;
        private DriveReservationService driveReservationService;
        private VehicleService vehicleService;

        private int groupPassengerNumber = 0;

        public GroupDriveReservationService(IGroupDriveReservationRepository groupReservationRepository,
            DriveReservationService driveReservationRepository,
            VehicleService vehicleService)
        {
            this.groupReservationRepository = groupReservationRepository;
            this.driveReservationService = driveReservationRepository;
            this.vehicleService = vehicleService;
        }

        public void ManageGroupDrives()
        {
            foreach (GroupDriveReservation reservation in groupReservationRepository.GetPendingReservations())
            {
                groupPassengerNumber = 0;
                ProcessReservation(reservation);
            }
        }

        private void ProcessReservation(GroupDriveReservation reservation)
        {
            List<Vehicle> compatibleVehicles = GetCompatibleVehicles(reservation);
            groupPassengerNumber = reservation.PassengerNumber;

            if (groupPassengerNumber > compatibleVehicles.Sum(v => v.Capacity))
            {
                RejectReservation(reservation);
                return;
            }

            foreach (Vehicle vehicle in compatibleVehicles)
            {
                AddDriveReservation(reservation, vehicle);
                if (vehicle == compatibleVehicles.Last() || groupPassengerNumber <= 0)
                {
                    AcceptReservation(reservation);
                    break;
                }
            }
        }

        private List<Vehicle> GetCompatibleVehicles(GroupDriveReservation reservation)
        {
            return vehicleService.GetCompatibleVehicles(reservation);
        }

        private void RejectReservation(GroupDriveReservation reservation)
        {
            groupReservationRepository.UpdateReservation(
                reservation.Id,
                GroupDriveStatus.Rejected,
                DateTime.Now.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
            );
        }

        private void AcceptReservation(GroupDriveReservation reservation)
        {
            groupReservationRepository.UpdateReservation(
                reservation.Id,
                GroupDriveStatus.Processed,
                DateTime.Now.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
            );
        }

        private void AddDriveReservation(GroupDriveReservation reservation, Vehicle vehicle)
        {
            DriveReservation driveReservation = new DriveReservation(
                reservation.UserId,
                reservation.StartAddressId,
                reservation.EndAddressId,
                vehicle.DriverId,
                reservation.DepartureTime.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
            );
            groupPassengerNumber = groupPassengerNumber - vehicle.Capacity;
            driveReservationService.Add(driveReservation);
        }

        public List<GroupDriveReservation> GetAll()
        {
            return groupReservationRepository.GetAll();
        }

        public GroupDriveReservation? GetById(int id)
        {
            return groupReservationRepository.GetById(id);
        }

        public void UpdateReservation(int reservationId, GroupDriveStatus status, string date)
        {
            groupReservationRepository.UpdateReservation(reservationId, status, date);
        }

        public GroupDriveReservation Add(GroupDriveReservation groupDriveReservation)
        {
            return groupReservationRepository.Add(groupDriveReservation);
        }

        public void Delete(GroupDriveReservation groupDriveReservation)
        {
            groupReservationRepository.Delete(groupDriveReservation);
        }

        public GroupDriveReservation Update(GroupDriveReservation groupDriveReservation)
        {
            return groupReservationRepository.Update(groupDriveReservation);
        }
    }
}
