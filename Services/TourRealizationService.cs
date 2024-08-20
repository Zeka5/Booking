using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Repository;
using BookingApp.WPF.View;
using BookingApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourRealization = BookingApp.Domain.Model.TourRealization;

namespace BookingApp.Services
{
    public class TourRealizationService
    {
        private ITourRealizationRepository tourRealizationRepository;
        private TourReservationService tourReservationService;
        private TourService tourService;
        private LanguageService languageService;
        private LocationService locationService;
        private CheckPointService checkPointService;
        private ImageService imageService;

        public ObservableCollection<TourRealizationDto> tourRealizations; 
        public TourRealizationService(ITourRealizationRepository tourRealizationRepository,TourService tourService,LocationService locationService,LanguageService languageService,TourReservationService tourReservationService,CheckPointService checkPointService /*,ImageService imageService*/)
        {
            imageService = new ImageService();
            tourRealizations = new ObservableCollection<TourRealizationDto>();

            this.tourRealizationRepository = tourRealizationRepository;
            this.tourService= tourService;
            this.languageService = languageService;
            this.locationService = locationService;
            this.tourReservationService = tourReservationService;
            this.checkPointService = checkPointService;
            //this.imageService = imageService;
        }

        public List<DateTime> GetBusyDays(int userId) 
        {
            List<DateTime> busyDays=new List<DateTime>();
            foreach(var tourRealization in GetAllByUser(userId))
            {
                if(tourRealization.TourState.ToString().Equals("None") && tourRealization.LastCheckPoint == -1)
                {
                    busyDays.Add(tourRealization.StartTime.Date);
                }
            }
            return busyDays;
        }

        public TourRealization FindStartedTour(int userId)
        {
            foreach (TourRealization tourRealization in tourRealizationRepository.GetAll())
            {
                int realUserId=tourService.GetUserId(tourRealization.TourId);
                if (tourRealization.TourState.ToString() == "Started" && tourRealization.LastCheckPoint != -1 && userId==realUserId)
                {
                    return tourRealization;
                }
            }
            return null;
        }

        public List<Tour> GetTodayTours(int userId)
        {
            List<Tour> tours = new List<Tour>(); 
            foreach (TourRealization tourRealization in tourRealizationRepository.GetAll())
            {
                int realUserId = tourService.GetUserId(tourRealization.TourId);
                if (tourRealization.StartTime.Date == DateTime.Now.Date && tourRealization.TourState.ToString() == "None" && userId==realUserId)
                {
                    tours.Add(tourService.GetById(tourRealization.TourId));
                }
            }
            tours = tours.GroupBy(x => x.Id).Select(x => x.First()).ToList();
            return tours;
        }

        public List<TourRealization> GetTourStarts(int tourId,string status)
        {
            List<TourRealization> tourStarts = new List<TourRealization>();
            foreach(var tourRealization in tourRealizationRepository.GetAll())
            {
                if(tourRealization.TourId == tourId && tourRealization.TourState.ToString().Equals(status))
                {
                    tourStarts.Add(tourRealization);
                }
            }
            return tourStarts;
        }

        public List<Tour> GetAllUpcomingTours(int userId)
        {
            List<Tour> tours= new List<Tour>();
            foreach(var tourRealization in tourRealizationRepository.GetAll())
            {
                int realUserId=tourService.GetUserId(tourRealization.TourId);
                if (tourRealization.TourState.ToString().Equals("None") && realUserId==userId)
                {
                    tours.Add(tourService.GetById(tourRealization.TourId));
                }
            }
            tours = tours.GroupBy(x => x.Id).Select(x => x.First()).ToList();
            return tours;
        }

        public List<TourRealization> GetAllFinishedTours(int userId, string year = "All time")
        {
            bool isAllTime = year.Equals("All time");
            bool isNumber = int.TryParse(year, out int tourYear);

            var tourRealizations = new List<TourRealization>();
            foreach (var tourRealization in tourRealizationRepository.GetAll())
            {
                int realUserId=tourService.GetUserId(tourRealization.TourId);
                if (tourRealization.TourState.ToString().Equals("Finished") && realUserId == userId &&
                    (isAllTime || (isNumber && tourRealization.StartTime.Year == tourYear)))
                {
                    tourRealizations.Add(tourRealization);
                }
            }
            return tourRealizations;
        }

        public ObservableCollection<TourRealizationDto> LoadFinishedTourRealizations()
        {
            var usersTourReservations = tourReservationService.GetAll().Where(tourReservation => tourReservation.UserId == SignInForm.curretnUserId);

            foreach (var tourReservation in usersTourReservations)
            {
                if (tourRealizationRepository.GetById(tourReservation.TourRealizationId).TourState == State.Finished && tourReservation.IsRated == Rated.NotRated)
                {
                    AddFinishedTourRealizationDto(tourRealizationRepository.GetById(tourReservation.TourRealizationId));
                }
            }
            return tourRealizations;
        }
        public ObservableCollection<TourRealizationDto> LoadAcitveTourRealizations()
        {
            tourRealizations.Clear();

            var usersTourReservations = tourReservationService.GetAll().Where(tourReservation => tourReservation.UserId == SignInForm.curretnUserId);

            foreach (var tourReservation in usersTourReservations)
            {
                if (tourRealizationRepository.GetById(tourReservation.TourRealizationId).TourState == State.Started)
                {
                    AddActiveTourRealizationDto(tourRealizationRepository.GetById(tourReservation.TourRealizationId));
                }
            }
            return tourRealizations;
        }
        private void AddActiveTourRealizationDto(TourRealization tourRealization)
        {
            var tour = tourService.GetById(tourRealization.TourId);
            var location = locationService.GetById(tour.LocationId);
            string checkpoint;
            string imagePath = imageService.GetFirstByEntityAndType(tour.Id, ResourceType.Tour).Path;
            var endTime = tourRealization.StartTime.AddDays(tour.Duration);

            if (tourRealization.LastCheckPoint == -1)
            {
                checkpoint = locationService.GetById(tourService.GetById(tourRealization.TourId).LocationId).City;
            }
            else
            {
                checkpoint = checkPointService.GetById(tourRealization.LastCheckPoint).Name;
            }
            tourRealizations.Add(new TourRealizationDto(tourRealization, tour.Name, location.Country, location.City, checkpoint, imagePath, endTime));
        }
        public void AddFinishedTourRealizationDto(TourRealization tourRealization)
        {
            var tour = tourService.GetById(tourRealization.TourId);
            var location = locationService.GetById(tour.LocationId);
            var endTime = tourRealization.StartTime.AddDays(tour.Duration);
            string imagePath = imageService.GetFirstByEntityAndType(tour.Id, ResourceType.Tour).Path;

            tourRealizations.Add(new TourRealizationDto(tourRealization, tour.Name, location.Country, location.City, imagePath, endTime));
        }
        public TourDto MakeTourDto(int tourId) {
            Tour tour = tourService.GetById(tourId);
            Location location = locationService.GetById(tour.LocationId);
            Language language = languageService.GetById(tour.LanguageId);
            return new TourDto(tour, location, language);
        }

        public int GetExpectedTouristNumber(int tourRealizationId)
        {
            int sum = 0;
            foreach(var tourReservation in tourReservationService.GetAll())
            {
                if (tourReservation.TourRealizationId == tourRealizationId)
                {
                    sum += tourReservation.VisitorsCount;
                }
            }
            return sum;
        }

        public void UpdateTourRealizationState(int id, string state)
        {
            TourRealization tourRealization = tourRealizationRepository.GetById(id);
            if (state.Equals("Started"))
            {
                tourRealization.TourState = State.Started;
            }
            else if (state.Equals("Cancelled"))
            {
                tourRealization.TourState = State.Cancelled;
            }
            else
            {
                tourRealization.TourState = State.Finished;
            }
            tourRealizationRepository.Update(tourRealization);
        }

        public void UpdateLastCheckPoint(int tourRealizationId, int checkPointIndex)
        {
            TourRealization tourRealization = tourRealizationRepository.GetById(tourRealizationId);
            tourRealization.LastCheckPoint = checkPointIndex;
            tourRealizationRepository.Update(tourRealization);
        }

        public int GetTourIdById(int id) {
            return tourRealizationRepository.GetById(id).TourId;
        }

        public int GetLastCheckPoint(int id)
        {
            return tourRealizationRepository.GetById(id).LastCheckPoint;
        }

        public void Save(DateTime date, int capacity)
        {
            Domain.Model.TourRealization tourRealization = new TourRealization(tourService.GetLastId(), date, capacity);
            tourRealizationRepository.Add(tourRealization);
        }

        public TourRealization GetLast()
        {
            return tourRealizationRepository.GetAll()[^1];
        }

        public void UpdateAvailability(int id)
        {
            TourRealization tourRealization=tourRealizationRepository.GetById(id);
            tourRealization.Availability = 0;
            tourRealizationRepository.Update(tourRealization);
        }

        public List<TourRealization> GetAllByUser(int userId)
        {
            List<TourRealization> personalTourRealizations = new List<TourRealization>();
            foreach (var tourRealization in tourRealizationRepository.GetAll())
            {
                int realUserId=tourService.GetUserId(tourRealization.TourId);
                if (realUserId == userId)
                {
                    personalTourRealizations.Add(tourRealization);
                }
            }
            return personalTourRealizations;
        }

        public List<TourRealization> GetAllFinishedFromPastYearByUser(int userId)
        {
            List<TourRealization> personalTourRealizations = new List<TourRealization>();
            foreach (var tourRealization in tourRealizationRepository.GetAll())
            {
                int realUserId = tourService.GetUserId(tourRealization.TourId);
                if (realUserId == userId && IsTourFinishedandFromPastYear(tourRealization.TourState.ToString(),tourRealization.StartTime))
                {
                    personalTourRealizations.Add(tourRealization);
                }
            }
            return personalTourRealizations;
        }

        private bool IsTourFinishedandFromPastYear(string tourState,DateTime startTime)
        {
            return tourState.Equals("Finished") && startTime.Date >= DateTime.Now.AddMonths(-12).Date && startTime.Date<=DateTime.Now.Date;
        }

        public Language GetLanguageById(int id)
        {
            TourRealization tourRealization = tourRealizationRepository.GetById(id);
            Tour tour = tourService.GetById(tourRealization.TourId);
            return languageService.GetById(tour.LanguageId);
        }
    }
}
