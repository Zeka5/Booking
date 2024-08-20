using BookingApp.Domain.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IComplexTourRequestRepository
    {
        List<ComplexTourRequest> GetAll();
        ComplexTourRequest GetById(int id);
        int NextId();
        ComplexTourRequest Add(ComplexTourRequest complexTourRequest);
        ComplexTourRequest Update(ComplexTourRequest complexTourRequest);
        void Delete(ComplexTourRequest complexTourRequest);
    }
}
