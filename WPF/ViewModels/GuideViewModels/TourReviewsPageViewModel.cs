using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Services;
using BookingApp.Utilities;
using BookingApp.WPF.View;
using BookingApp.WPF.View.Guide;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.GuideViewModels
{
    public class TourReviewsPageViewModel : INotifyPropertyChanged
    {
        public NavigationService NavService { get; set; }
        public MyICommand<TourRatingDto> ReportCommand {  get; set; }
        public ObservableCollection<TourDto> FinishedTours {  get; set; }

        private TourDto selectedFinishedTour;

        public TourDto SelectedFinishedTour
        {
            get { return selectedFinishedTour; }
            set
            {
                selectedFinishedTour = value;
                OnPropertyChanged(nameof(SelectedFinishedTour));
                LoadTourReviews(value);
            }
        }

        public ObservableCollection<TourRatingDto> TourReviews { get; set; }

        public TourRatingDto SelectedTourRating { get; set; }

        private TourService tourService;
        private LocationService locationService;
        private LanguageService languageService;
        private ImageService imageService;
        private TourGuestService tourGuestService;
        private TourRealizationService tourRealizationService;
        private TourRatingService tourRatingService;

        public TourReviewsPageViewModel(NavigationService navService) {
            ReportCommand=new MyICommand<TourRatingDto>(Execute_ReportCommand);
            FinishedTours = new ObservableCollection<TourDto>();
            TourReviews = new ObservableCollection<TourRatingDto>();
            SelectedTourRating= new TourRatingDto();
            tourService = new TourService(Injector.CreateInstance<ITourRepository>());
            locationService = new LocationService(Injector.CreateInstance<ILocationRepository>());
            languageService = new LanguageService(Injector.CreateInstance<ILanguageRepository>());
            imageService = new ImageService(Injector.CreateInstance<IImageRepository>());
            tourGuestService = new TourGuestService(Injector.CreateInstance<ITourGuestRepository>(),new TourReservationService(Injector.CreateInstance<ITourReservationRepository>()));
            tourRealizationService = new TourRealizationService(Injector.CreateInstance<ITourRealizationRepository>(),tourService,locationService,languageService, new TourReservationService(Injector.CreateInstance<ITourReservationRepository>()), new CheckPointService(Injector.CreateInstance<ICheckPointRepository>()));
            tourRatingService = new TourRatingService(Injector.CreateInstance<ITourRatingRepository>(),tourGuestService);
            NavService = navService;
            LoadFinishedTours();
        }

        private void Execute_ReportCommand(TourRatingDto tourRating)
        {
            tourRating.ValidityChecked = true;
            tourRating.Valid = "NOT VALID";
            tourRatingService.UpdateValidity(tourRating);
        }

        private void LoadFinishedTours()
        {
            foreach(var tourRealization in tourRealizationService.GetAllFinishedTours(SignInForm.curretnUserId))
            {
                FinishedTours.Add(MakeTourDto(tourRealization));
            }
        }

        public void LoadTourReviews(TourDto tour)
        {
            TourReviews.Clear();
            foreach (var tourRating in tourRatingService.GetAllByTourRealizationId(tour.TourStart.Id))
            {
                TourReviews.Add(MakeTourRatingDto(tourRating));
            }
        }

        private TourRatingDto MakeTourRatingDto(TourRating tourRating)
        {
            TourRatingDto tourRatingDto = new TourRatingDto(tourRating);
            TourGuest tourGuest = tourGuestService.GetById(tourRating.TourGuestId);
            tourRatingDto.TourGuestName = tourGuest.FullName;
            tourRatingDto.CheckPointName = tourGuest.CheckPointName;
            if (imageService.GetFirstByEntityAndType(tourRatingDto.Id, ResourceType.TourRating) == null) tourRatingDto.ImagePath = "";
            else tourRatingDto.ImagePath = imageService.GetFirstByEntityAndType(tourRatingDto.Id, ResourceType.TourRating).Path;
            return tourRatingDto;
        }

        private TourDto MakeTourDto(TourRealization tourRealization)
        {
            Tour tour = tourService.GetById(tourRealization.TourId);
            Location location = locationService.GetById(tour.LocationId);
            Language language = languageService.GetById(tour.LanguageId);
            TourDto tourDto = new TourDto(tour, location, language);
            tourDto.TourStart = new TourRealizationDto(tourRealization);
            if (imageService.GetFirstByEntityAndType(tourDto.Id, ResourceType.Tour) == null) tourDto.ImagePath = "";
            else tourDto.ImagePath = imageService.GetFirstByEntityAndType(tourDto.Id, ResourceType.Tour).Path;
            return tourDto;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
