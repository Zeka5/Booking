using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class TourRequestService
    {
        private ITourRequestRepository tourRequestRepository;
        private TourRealizationService tourRealizationService;

        public TourRequestService(ITourRequestRepository tourRequestRepository,TourRealizationService tourRealizationService)
        {
            this.tourRequestRepository = tourRequestRepository;
            this.tourRealizationService= tourRealizationService;
        }

        public List<TourRequest> GetAll()
        {
            return tourRequestRepository.GetAll();
        }

        public List<TourRequest> GetAllRequestsByStatus(string status)
        {
            return tourRequestRepository.GetAll().FindAll(tr => tr.Status.ToString().Equals(status));
        }

        public List<DateTime> FindUserBusyDays(DateTime startDate,DateTime endDate,int userId)
        {
            List<DateTime> busyDays= new List<DateTime>();
            foreach(var date in tourRealizationService.GetBusyDays(userId))
            {
                if (date >= startDate && date <= endDate) busyDays.Add(date);
            }
            return busyDays;
        }

        public DateTime FindUserFirstFreeDay(DateTime startDate,DateTime endDate,int userId)
        {
            List<DateTime> busyDays= FindUserBusyDays(startDate,endDate,userId);
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (!busyDays.Contains(date))
                    return date;
            }
            return new DateTime();
        }

        public void Update(TourRequest tourRequest) 
        { 
            tourRequestRepository.Update(tourRequest);
        }

        public int FindAllRequestsByYearandParametar(int year,int id,string parametarType)
        {
            if (parametarType.Equals("Language")) return tourRequestRepository.GetAll().FindAll(tr=>tr.CreationDate.Year==year && tr.LanguageId==id).Count;
            else return tourRequestRepository.GetAll().FindAll(tr => tr.CreationDate.Year == year && tr.LocationId == id).Count;
        }

        public int FindAllRequestsByMonthandParametar(int month,int year,int id, string parametarType)
        {
            if (parametarType.Equals("Language")) return tourRequestRepository.GetAll().FindAll(tr => tr.CreationDate.Year == year && tr.CreationDate.Month==month && tr.LanguageId == id).Count;
            else return tourRequestRepository.GetAll().FindAll(tr => tr.CreationDate.Year == year && tr.CreationDate.Month == month && tr.LocationId == id).Count;
        }

        public int FindMostWantedLanguage()
        {
            Dictionary<int, int> languageCount = new Dictionary<int, int>();
            foreach (var tourRequest in GetAllFromPastYear())
            {
                if (languageCount.ContainsKey(tourRequest.LanguageId)) languageCount[tourRequest.LanguageId]++;
                else languageCount[tourRequest.LanguageId] = 1;
            }
            int maxCount = languageCount.Max(lid => lid.Value);
            return languageCount.First(kvp => kvp.Value == maxCount).Key;
        }

        public int FindMostWantedLocation() 
        {
            Dictionary<int, int> locationCount = new Dictionary<int, int>();
            foreach (var tourRequest in GetAllFromPastYear())
            {
                if (locationCount.ContainsKey(tourRequest.LocationId)) locationCount[tourRequest.LocationId]++;
                else locationCount[tourRequest.LocationId] = 1;
            }
            int maxCount = locationCount.Max(lid => lid.Value);
            return locationCount.First(kvp => kvp.Value == maxCount).Key;
        }

        private List<TourRequest> GetAllFromPastYear()
        {
            DateTime thisYear=DateTime.Now.Date;
            DateTime pastYear = thisYear.AddYears(-1);
            List<TourRequest> requestsInYearSpan=new List<TourRequest>();
            foreach(var tourRequest in tourRequestRepository.GetAll())
            {
                if(tourRequest.CreationDate.Date >= pastYear && tourRequest.CreationDate.Date <= thisYear)
                {
                    requestsInYearSpan.Add(tourRequest);
                }
            }
            return requestsInYearSpan;
        }
      
        public TourRequest GetLast()
        {
            return tourRequestRepository.GetAll().LastOrDefault();
        }
      
        public void Add(TourRequest tourRequest)
        {
            tourRequestRepository.Add(tourRequest);
        }
    }
}
