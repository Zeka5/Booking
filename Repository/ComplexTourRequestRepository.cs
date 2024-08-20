using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class ComplexTourRequestRepository:IComplexTourRequestRepository
    {
        private const string FilePath = "../../../Resources/Data/complexTourRequests.csv";

        private readonly Serializer<ComplexTourRequest> serializer;

        private List<ComplexTourRequest> complexTourRequests;

        public ComplexTourRequestRepository()
        {
            serializer = new Serializer<ComplexTourRequest>();
            complexTourRequests = serializer.FromCSV(FilePath);
        }
        public List<ComplexTourRequest> GetAll()
        {
            return serializer.FromCSV(FilePath);
        }
        public ComplexTourRequest GetById(int id)
        {
            complexTourRequests = serializer.FromCSV(FilePath);
            return complexTourRequests.Find(ctr => ctr.Id == id);
        }
        public int NextId()
        {
            complexTourRequests = serializer.FromCSV(FilePath);
            if (complexTourRequests.Count < 1)
            {
                return 1;
            }
            return complexTourRequests.Max(t => t.Id) + 1;
        }

        public ComplexTourRequest Add(ComplexTourRequest complexTourRequest)
        {
            complexTourRequests = serializer.FromCSV(FilePath);
            complexTourRequest.Id = NextId();
            complexTourRequests.Add(complexTourRequest);
            serializer.ToCSV(FilePath, complexTourRequests);
            return complexTourRequest;
        }
        public ComplexTourRequest Update(ComplexTourRequest complexTourRequest)
        {
            complexTourRequests = serializer.FromCSV(FilePath);
            ComplexTourRequest current = complexTourRequests.Find(ctr => ctr.Id == complexTourRequest.Id);
            int index = complexTourRequests.IndexOf(current);
            complexTourRequests.Remove(current);
            complexTourRequests.Insert(index, complexTourRequest);       // keep ascending order of ids in file 
            serializer.ToCSV(FilePath, complexTourRequests);
            return complexTourRequest;
        }


        public void Delete(ComplexTourRequest complexTourRequest)
        {
            complexTourRequests = serializer.FromCSV(FilePath);
            ComplexTourRequest founded = complexTourRequests.Find(ctr => ctr.Id == complexTourRequest.Id);
            complexTourRequests.Remove(founded);
            serializer.ToCSV(FilePath, complexTourRequests);
        }
    }
}