using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class ComplexTourRequestService
    {
        private IComplexTourRequestRepository complexTourRequestRepository;

        public ComplexTourRequestService(IComplexTourRequestRepository complexTourRequestRepository)
        {
            this.complexTourRequestRepository = complexTourRequestRepository;
        }
    }
}
