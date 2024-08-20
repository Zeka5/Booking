using BookingApp.Domain.Model;
using BookingApp.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IReservationChangeRequestRepository
    {
        public void Add(ReservationChangeRequest reservationChangeRequest);
        public void Update(ReservationChangeRequest request);
        public ReservationChangeRequest? GetById(int id);
        public List<ReservationChangeRequest> GetAllByUser(User user);
        public List<ReservationChangeRequest> GetAll();
        public List<ReservationChangeRequest> GetAllResponsed();
        public bool IsClickable(int id);
        void Subscribe(IObserver observer);
        int RequestCountByReservationId(int id);
    }
}
