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
    public class ReservationChangeRequestRepository : IReservationChangeRequestRepository
    {
        private const string FilePath = "../../../Resources/Data/reservationChangeRequest.csv";
        private readonly Serializer<ReservationChangeRequest> serializer;
        private List<ReservationChangeRequest> reservationChangeRequests;
        private AccommodationReservationRepository accommodationReservationRepository;
        public Subject ReservationChangeRequestSubject;

        public ReservationChangeRequestRepository()
        {
            serializer = new Serializer<ReservationChangeRequest>();
            reservationChangeRequests = serializer.FromCSV(FilePath);
            ReservationChangeRequestSubject = new Subject();
            accommodationReservationRepository = new AccommodationReservationRepository();
        }

        private int GenerateId()
        {
            if (reservationChangeRequests.Count == 0) return 1;
            else return reservationChangeRequests[^1].Id + 1;
        }

        public void Add(ReservationChangeRequest reservationChangeRequest)
        {
            reservationChangeRequest.Id = GenerateId();
            reservationChangeRequests.Add(reservationChangeRequest);
            serializer.ToCSV(FilePath, reservationChangeRequests);
            ReservationChangeRequestSubject.NotifyObservers();
        }

        public void Update(ReservationChangeRequest request)
        {
            var oldRequest = GetById(request.Id);
            if (oldRequest is null) return;
            oldRequest.Status = request.Status;
            oldRequest.Comment = request.Comment;
            //PROSIRITI PO POTREBI

            serializer.ToCSV(FilePath, reservationChangeRequests);
            ReservationChangeRequestSubject.NotifyObservers();
        }

        public bool IsClickable(int id) {
            return reservationChangeRequests.Any( x => x.AccommodationReservationId==id && x.Status==Status.Waiting);
        }

        public ReservationChangeRequest? GetById(int id)
        {
            return reservationChangeRequests.FirstOrDefault(request => request.Id == id);
        }
        public List<ReservationChangeRequest> GetAll() {
            return reservationChangeRequests;
        }
        public List<ReservationChangeRequest> GetAllByUser(User user)
        {
            List<ReservationChangeRequest> result = new List<ReservationChangeRequest> ();
            foreach (ReservationChangeRequest reservationChangeRequest in reservationChangeRequests)
            {
                AccommodationReservation? accommodationReservation = accommodationReservationRepository.GetById(reservationChangeRequest.AccommodationReservationId);
                if (accommodationReservation.UserId == user.Id) { 
                    result.Add(reservationChangeRequest);
                }
            }
            return result;         
        }

        public List<ReservationChangeRequest> GetAllResponsed() {
            return reservationChangeRequests.FindAll(x => x.Status != Status.Waiting);
        }

        public void Subscribe(IObserver observer)
        {
            ReservationChangeRequestSubject.Subscribe(observer);
        }

        public int RequestCountByReservationId(int id)
        {
            return reservationChangeRequests.Count(request => request.AccommodationReservationId == id);
        }
    }
}
