using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Observer;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.Repository
{
    public class GroupDriveReservationRepository : IGroupDriveReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/groupDriveReservations.csv";

        private readonly Serializer<GroupDriveReservation> serializer;

        private List<GroupDriveReservation> groupDriveReservations;
        public Subject GroupDriveReservationSubject;

        public GroupDriveReservationRepository()
        {
            GroupDriveReservationSubject = new Subject();
            serializer = new Serializer<GroupDriveReservation>();
            groupDriveReservations = serializer.FromCSV(FilePath);
        }

        public void UpdateReservation(int reservationId, GroupDriveStatus status, string date)
        {
            GroupDriveReservation? reservation = GetById(reservationId);
            if (reservation == null) { return; }

            reservation.Status = status;
            reservation.ReservationTime = DateTime.ParseExact(date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

            serializer.ToCSV(FilePath, groupDriveReservations);
        }

        public GroupDriveReservation Add(GroupDriveReservation groupDriveReservation)
        {
            groupDriveReservations = serializer.FromCSV(FilePath);
            groupDriveReservation.Id = NextId();
            groupDriveReservations.Add(groupDriveReservation);
            serializer.ToCSV(FilePath, groupDriveReservations);
            return groupDriveReservation;
        }

        public void Delete(GroupDriveReservation groupDriveReservation)
        {
            throw new NotImplementedException();
        }

        public List<GroupDriveReservation> GetAll()
        {
            return serializer.FromCSV(FilePath);
        }

        public GroupDriveReservation? GetById(int id)
        {
            return groupDriveReservations.Find(d => d.Id == id);
        }

        public List<GroupDriveReservation> GetPendingReservations()
        {
            List<GroupDriveReservation> pendingReservations = new List<GroupDriveReservation>();
            groupDriveReservations = serializer.FromCSV(FilePath);

            foreach (GroupDriveReservation reservation in groupDriveReservations)
            {
                if(reservation.Status == GroupDriveStatus.Pending) pendingReservations.Add(reservation);
            }

            return pendingReservations;
        }

        public GroupDriveReservation Update(GroupDriveReservation groupDriveReservation)
        {
            groupDriveReservations = serializer.FromCSV(FilePath);
            GroupDriveReservation current = groupDriveReservations.Find(c => c.Id == groupDriveReservation.Id);
            int index = groupDriveReservations.IndexOf(current);
            groupDriveReservations.Remove(current);
            groupDriveReservations.Insert(index, groupDriveReservation);       // keep ascending order of ids in file 
            serializer.ToCSV(FilePath, groupDriveReservations);
            return groupDriveReservation;
        }

        public int NextId()
        {
            groupDriveReservations = serializer.FromCSV(FilePath);
            if (groupDriveReservations.Count < 1)
            {
                return 1;
            }
            return groupDriveReservations.Max(d => d.Id) + 1;
        }
    }
}
