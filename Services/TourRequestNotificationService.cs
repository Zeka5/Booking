using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class TourRequestNotificationService
    {
        private ITourRequestNotificationRepository tourRequestNotificationRepository;
        private TourRequestService tourRequestService;
        public TourRequestNotificationService(ITourRequestNotificationRepository tourRequestNotificationRepository,TourRequestService tourRequestService) 
        { 
            this.tourRequestNotificationRepository = tourRequestNotificationRepository;
            this.tourRequestService = tourRequestService;
        }


        public void Create(TourRequest tourRequest,string tourName,string tourStart,int guideId)
        {
            tourRequest.Status=STATUS.Accepted;
            tourRequest.GuideId = guideId;
            tourRequestService.Update(tourRequest);
            TourRequestNotification tourRequestNotification=new TourRequestNotification(tourRequest.Id,"Your tour request is accepted by Dimitrije, name of tour is "+tourName+" and it will be held on "+tourStart+"!",DateTime.Now);
            tourRequestNotificationRepository.Add(tourRequestNotification);
        }
    }
}
