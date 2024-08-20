using BookingApp.Domain.Model;
using BookingApp.Dto;
using BookingApp.Services;
using BookingApp.Utilities;
using BookingApp.WPF.View.Guest;
using BookingApp.WPF.View.Guide;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using System.Windows;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.WPF.View;

namespace BookingApp.WPF.ViewModels.GuideViewModels
{
    public class HomePageViewModel:INotifyPropertyChanged
    {
        public NavigationService NavService {  get; set; }
        public DateTime DateToday { get; set; }
        public ObservableCollection<TourDto> TodayTours { get; set; }
        public TourDto SelectedTour{get;set;}
        public MyICommand<TourDto> StartTourCommand {  get; set; }
        public MyICommand CreateTourCommand { get; set; }
        private TourRealizationService tourRealizationService;
        private TourReservationService tourReservationService;
        private TourService tourService;
        private LocationService locationService;
        private LanguageService languageService;
        private ImageService imageService;
        private bool startedTourExist;
        public HomePageViewModel(NavigationService navService) { 
            NavService = navService;
            DateToday=DateTime.Now.Date;
            StartTourCommand = new MyICommand<TourDto>(Execute_StartTourCommand);
            CreateTourCommand = new MyICommand(Execute_CreateTourCommand);
            TodayTours = new ObservableCollection<TourDto>();
            SelectedTour = new TourDto();
            tourService = new TourService(Injector.CreateInstance<ITourRepository>());
            locationService = new LocationService(Injector.CreateInstance<ILocationRepository>());
            languageService = new LanguageService(Injector.CreateInstance<ILanguageRepository>());
            imageService = new ImageService(Injector.CreateInstance<IImageRepository>());
            tourRealizationService = new TourRealizationService(Injector.CreateInstance<ITourRealizationRepository>(),tourService,locationService,languageService, new TourReservationService(Injector.CreateInstance<ITourReservationRepository>()), new CheckPointService(Injector.CreateInstance<ICheckPointRepository>()));
            tourReservationService = new TourReservationService(Injector.CreateInstance<ITourReservationRepository>());
            startedTourExist=false;
            LoadTodayTours();
        }

        private void Execute_CreateTourCommand()
        {
            NavService.Navigate(new AddingTourPage(NavService));
        }

        private void Execute_StartTourCommand(TourDto tour)
        {
            if (tour.TourStart == null)
            {
                MessageBox.Show("Please pick start time of the tour!");
                return;
            }

            if (!StartTour(tour.TourStart.Id))
            {
                MessageBox.Show("You don't have any reservations for this tour!");
                return;
            }

            NavService.Navigate(new TourTrackingPage(NavService,tour.TourStart.Id));
        }

        private void LoadTodayTours()
        {
            TodayTours.Clear();
            if (tourRealizationService.FindStartedTour(SignInForm.curretnUserId) != null)
            {
                TodayTours.Add(MakeStartedTour(tourRealizationService.FindStartedTour(SignInForm.curretnUserId)));
                startedTourExist = true;
                return;
            }
            foreach (var tour in tourRealizationService.GetTodayTours(SignInForm.curretnUserId)) 
            {
                TourDto todayTour=LoadTourStartTimes(tour);
                TodayTours.Add(todayTour);
            }        
        }

        private TourDto LoadTourStartTimes(Tour tour)
        {
            TourDto todayTour = MakeTodayTour(tour.Id);
            foreach(var tourStart in tourRealizationService.GetTourStarts(tour.Id,"None"))
            {
                if (tourStart.StartTime.Date == DateTime.Now.Date)
                {
                    todayTour.TourRealizations.Add(new TourRealizationDto(tourStart));
                }
            }
            return todayTour;
        }

        public bool StartTour(int tourRealizationId)
        {
                if (startedTourExist)
                {
                    return true;
                }
                else
                {
                    if (CanTourStart(tourRealizationId))
                    {
                        return true;
                    }
                    return false;
                }
        }

        private bool CanTourStart(int tourRealizationId)
        {
            if (tourReservationService.DoesTourReservationExist(tourRealizationId))
            {
                tourRealizationService.UpdateTourRealizationState(tourRealizationId, "Started");
                return true;
            }
            return false;
        }

        private TourDto MakeStartedTour(TourRealization tourRealization)
        {
            Tour tour = tourService.GetById(tourRealization.TourId);
            Location location = locationService.GetById(tour.LocationId);
            Language language = languageService.GetById(tour.LanguageId);
            TourDto tourDto = new TourDto(tour, location, language);
            if (imageService.GetFirstByEntityAndType(tourDto.Id, ResourceType.Tour) == null) tourDto.ImagePath = "";
            else tourDto.ImagePath = imageService.GetFirstByEntityAndType(tourDto.Id, ResourceType.Tour).Path;
            tourDto.TourStart = new TourRealizationDto(tourRealization);
            tourDto.TourRealizations.Add(tourDto.TourStart);
            return tourDto;
        }

        private TourDto MakeTodayTour(int tourId)
        {
            Tour tour = tourService.GetById(tourId);
            Location location = locationService.GetById(tour.LocationId);
            Language language = languageService.GetById(tour.LanguageId);
            TourDto tourDto = new TourDto(tour, location, language);
            if (imageService.GetFirstByEntityAndType(tourDto.Id, ResourceType.Tour) == null) tourDto.ImagePath = "";
            else tourDto.ImagePath = imageService.GetFirstByEntityAndType(tourDto.Id, ResourceType.Tour).Path;
            return tourDto;
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }

        }
    }
}
