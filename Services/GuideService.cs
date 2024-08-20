using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class GuideService
    {
        private IGuideRepository guideRepository;
        public GuideService(IGuideRepository guideRepository)
        {
            this.guideRepository = guideRepository;
        }

        public Guide GetByUserId(int Id)
        {
            return guideRepository.GetByUserId(Id);
        }

        public void Update(Guide guide)
        {
            guideRepository.Update(guide);
        }
    }
}
