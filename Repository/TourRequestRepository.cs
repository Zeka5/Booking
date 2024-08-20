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
    public class TourRequestRepository:ITourRequestRepository
    {
        private const string FilePath = "../../../Resources/Data/tourRequests.csv";

        private readonly Serializer<TourRequest> serializer;

        private List<TourRequest> tourRequests;

        public TourRequestRepository()
        {
            serializer = new Serializer<TourRequest>();
            tourRequests = serializer.FromCSV(FilePath);
        }
        public List<TourRequest> GetAll()
        {
            return serializer.FromCSV(FilePath);
        }
        public TourRequest GetById(int id)
        {
            tourRequests = serializer.FromCSV(FilePath);
            return tourRequests.Find(tr => tr.Id == id);
        }
        public int NextId()
        {
            tourRequests = serializer.FromCSV(FilePath);
            if (tourRequests.Count < 1)
            {
                return 1;
            }
            return tourRequests.Max(t => t.Id) + 1;
        }

        public TourRequest Add(TourRequest tourRequest)
        {
            tourRequests = serializer.FromCSV(FilePath);
            tourRequest.Id = NextId();
            tourRequests.Add(tourRequest);
            serializer.ToCSV(FilePath, tourRequests);
            return tourRequest;
        }
        public TourRequest Update(TourRequest tourRequest)
        {
            tourRequests = serializer.FromCSV(FilePath);
            TourRequest current = tourRequests.Find(tr => tr.Id == tourRequest.Id);
            int index = tourRequests.IndexOf(current);
            tourRequests.Remove(current);
            tourRequests.Insert(index, tourRequest);       // keep ascending order of ids in file 
            serializer.ToCSV(FilePath, tourRequests);
            return tourRequest;
        }


        public void Delete(TourRequest tourRequest)
        {
            tourRequests = serializer.FromCSV(FilePath);
            TourRequest founded = tourRequests.Find(tr => tr.Id == tourRequest.Id);
            tourRequests.Remove(founded);
            serializer.ToCSV(FilePath, tourRequests);
        }
    }
}
