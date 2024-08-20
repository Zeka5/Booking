using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.WPF.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.TourGuestViewModels
{
    public class TourRequestViewModel : INotifyPropertyChanged
    {
        public TourRequestDto TourRequestDto { get; set; }
        public TourRequestTourGuest TourRequestTourGuest { get; set; }
        public List<LanguageDto> Languages { get; set; }
        public List<LocationDto> Locations { get; set; }
        public LocationDto SelectedLocation { get; set; }
        public LanguageDto SelectedLanguage { get; set; }
        public List<TourRequestTourGuest> TempTourRequestTourGuest { get; set; }
        public LanguageService languageService;
        public LocationService locationService;
        public TourRequestService tourRequestService;
        public TourRequestTourGuestService tourRequestTourGuestService;
        public int CurrentTourRequestTourGuest {  set; get; }
        public TourRequestViewModel()
        {
            languageService = new LanguageService(Injector.CreateInstance<ILanguageRepository>());
            locationService = new LocationService(Injector.CreateInstance<ILocationRepository>());
            tourRequestService = new TourRequestService(Injector.CreateInstance<ITourRequestRepository>(), new TourRealizationService(Injector.CreateInstance<ITourRealizationRepository>(), new TourService(Injector.CreateInstance<ITourRepository>()), new LocationService(Injector.CreateInstance<ILocationRepository>()), new LanguageService(Injector.CreateInstance<ILanguageRepository>()), new TourReservationService(Injector.CreateInstance<ITourReservationRepository>()), new CheckPointService(Injector.CreateInstance<ICheckPointRepository>())));
            tourRequestTourGuestService = new TourRequestTourGuestService(Injector.CreateInstance<ITourRequestTourGuestRepository>());

            TourRequestDto = new TourRequestDto();
            Languages = new List<LanguageDto>();
            Locations = new List<LocationDto>();
            SelectedLocation = new LocationDto();
            SelectedLanguage = new LanguageDto();
            TempTourRequestTourGuest = new List<TourRequestTourGuest>();
            TourRequestTourGuest = new TourRequestTourGuest();

            TourRequestDto.NumberOfGuests = 1;
            CurrentTourRequestTourGuest = 1;

            StartTime = DateTime.Now;
            EndTime = DateTime.Now;

            
            LoadLanguages();
            LoadLocations();
        }
        public DateTime startTime;
        public DateTime StartTime
        {
            get { return startTime; }
            set
            {
                if (value != startTime)
                {
                    startTime = value;
                    OnPropertyChanged("StartTime");
                }
            }
        }
        public DateTime endTime;
        public DateTime EndTime
        {
            get { return endTime; }
            set
            {
                if (value != endTime)
                {
                    endTime = value;
                    OnPropertyChanged("EndTime");
                }
            }
        }
        public void SetExpiredTourRequests()
        {
            foreach (var tourRequest in tourRequestService.GetAll())
            {
                if (DateOnly.FromDateTime(DateTime.Now.AddDays(2)) > tourRequest.StartDate && tourRequest.Status == STATUS.Pending)
                {
                    tourRequest.Status = STATUS.Expired;
                    tourRequestService.Update(tourRequest);
                }
            }
        }
        public void LoadLocations()
        {
            Locations.Clear();

            foreach (var location in locationService.GetAll())
            {
                this.Locations.Add(new LocationDto(location));
            }
        }
        public void LoadLanguages()
        {
            Languages.Clear();

            foreach (var language in languageService.GetAll())
            {
                this.Languages.Add(new LanguageDto(language));
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void NextTourRequestTourGuest()
        {
            if(CurrentTourRequestTourGuest != TourRequestDto.NumberOfGuests)
            {
                TempTourRequestTourGuest.Add(new TourRequestTourGuest(TourRequestTourGuest.FullName, TourRequestTourGuest.Age));
                CurrentTourRequestTourGuest++;

                ClearInputFields();
            }
            else
            {
                TempTourRequestTourGuest.Add(new TourRequestTourGuest(TourRequestTourGuest.FullName, TourRequestTourGuest.Age));
                MessageBox.Show("All tour guests done!");
            }
        }
        public void AddTourRequest()
        {
            TourRequest tourRequest = new TourRequest(SelectedLocation.Id, TourRequestDto.Description, SelectedLanguage.Id, TourRequestDto.NumberOfGuests, DateOnly.FromDateTime(StartTime), DateOnly.FromDateTime(EndTime), SignInForm.curretnUserId);
            tourRequestService.Add(tourRequest);
        }
        public void AddTourRequestTourGuests()
        {
            foreach (TourRequestTourGuest tourRequestTourGuest in TempTourRequestTourGuest)
            {
                tourRequestTourGuest.TourRequestId = tourRequestService.GetLast().Id;
                tourRequestTourGuestService.Add(tourRequestTourGuest);
            }
        }
        public void ConfirmTourRequest()
        {
            AddTourRequest();
            AddTourRequestTourGuests();
            MessageBox.Show("Tour request successfully sent");
        }
        public void DecreaseNumberOfTourGuests()
        {
            if (TourRequestDto.NumberOfGuests == 1)
            {
                return;
            }
            TourRequestDto.NumberOfGuests--;
        }
        public void IncreaseNumberOfTourGuests()
        {
            if (TourRequestDto.NumberOfGuests == 10)
            {
                return;
            }
            TourRequestDto.NumberOfGuests++;
        }
        private void ClearInputFields()
        {
            
        }
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
