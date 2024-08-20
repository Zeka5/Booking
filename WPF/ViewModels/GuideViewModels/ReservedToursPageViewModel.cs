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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using TourRealization = BookingApp.Domain.Model.TourRealization;

namespace BookingApp.WPF.ViewModels.GuideViewModels
{
    public class ReservedToursPageViewModel:INotifyPropertyChanged
    {
        public NavigationService NavService { get; set; }

        public MyICommand<TourDto> UpdateCanCancelCommand { get; set; }
        public MyICommand<TourDto> CancelTourCommand {  get; set; }

        public MyICommand NavigateToBestTourPage {  get; set; }
        public ObservableCollection<TourStatsDto> FinishedToursStats { get; set; }
        public ObservableCollection<TourDto> UpcomingTours { get; set; }
        public TourDto SelectedTour { get; set; }
        private TourService tourService;
        private LocationService locationService;
        private LanguageService languageService;
        private TourRealizationService tourRealizationService;
        private TourGuestService tourGuestService;
        private VoucherService voucherService;
        private ImageService imageService;

        private int selectedTabIndex;
        public int SelectedTabIndex
        {
            get { return selectedTabIndex; }
            set
            {
                selectedTabIndex = value;
                OnPropertyChanged("SelectedTabIndex");
                ChangePageTitle(value);
            }
        }

        private string pageTitle="Upcoming Tours";
        public string PageTitle
        {
            get { return pageTitle; }
            set
            {
                pageTitle = value;
                OnPropertyChanged("PageTitle"); 
            }
        }

        public ReservedToursPageViewModel(NavigationService navService) 
        {
            FinishedToursStats = new ObservableCollection<TourStatsDto>();
            UpcomingTours = new ObservableCollection<TourDto>();
            SelectedTour = new TourDto();
            tourService = new TourService(Injector.CreateInstance<ITourRepository>());
            locationService = new LocationService(Injector.CreateInstance<ILocationRepository>());
            languageService = new LanguageService(Injector.CreateInstance<ILanguageRepository>());
            imageService = new ImageService(Injector.CreateInstance<IImageRepository>());
            tourRealizationService = new TourRealizationService(Injector.CreateInstance<ITourRealizationRepository>(),tourService, locationService, languageService, new TourReservationService(Injector.CreateInstance<ITourReservationRepository>()), new CheckPointService(Injector.CreateInstance<ICheckPointRepository>()));
            tourGuestService = new TourGuestService(Injector.CreateInstance<ITourGuestRepository>(), new TourReservationService(Injector.CreateInstance<ITourReservationRepository>()));
            voucherService = new VoucherService(Injector.CreateInstance<IVoucherRepository>(), new TourReservationService(Injector.CreateInstance<ITourReservationRepository>()));
            NavService = navService;
            UpdateCanCancelCommand = new MyICommand<TourDto>(Execute_UpdateCanCancelCommand, CanExecute_UpdateCanCancelCommand);
            CancelTourCommand = new MyICommand<TourDto>(Execute_CancelTourCommand);
            NavigateToBestTourPage = new MyICommand(Execute_NavigateToBestTourPage);
            LoadTours();
            LoadFinishedTourStats();
        }

        private bool CanExecute_UpdateCanCancelCommand(TourDto tour)
        {
            if (tour != null)
            {
                return tour.TourStart != null;
            }
            return true;
        }

        private void Execute_UpdateCanCancelCommand(TourDto tour)
        {
            if (DateTime.ParseExact(tour.TourStart.StartTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture) > DateTime.Now.AddHours(48))
            {
                tour.CanCancel = true;
                return;//promenio
            }
            tour.CanCancel = false;
        }

        private void Execute_CancelTourCommand(TourDto tour)
        {
            voucherService.MakeVouchers(tour.TourStart.Id,12, "Received after a tour canceling", SignInForm.curretnUserId);
            tourRealizationService.UpdateTourRealizationState(tour.TourStart.Id, "Cancelled");
            FilterUpcomingTours(tour);
            MessageBox.Show("You cancelled a tour!");
        }

        public void FilterUpcomingTours(TourDto tour)
        {
            if (tour.TourRealizations.Count > 1)
            {
                tour.CanCancel = false;
                tour.TourRealizations.Remove(tour.TourStart);
            }
            else
            {
                UpcomingTours.Remove(tour);
            }
        }

        private void Execute_NavigateToBestTourPage()
        {
            NavService.Navigate(new BestTourPage(NavService));
        }

        private void LoadTours()
        {
            UpcomingTours.Clear();
            foreach (var tour in tourRealizationService.GetAllUpcomingTours(SignInForm.curretnUserId))
            {
                TourDto tourDto=LoadTourStarts(tour);
                if (tourDto.TourRealizations.Count > 0)
                {
                    UpcomingTours.Add(tourDto);
                }
            }
        }

        private TourDto LoadTourStarts(Tour tour)
        {
            TourDto tourDto = MakeUpcomingTour(tour.Id);
            foreach (var tourStart in tourRealizationService.GetTourStarts(tour.Id, "None"))
            {
                if (tourStart.StartTime.Date >= DateTime.Now.Date)
                {
                    tourDto.TourRealizations.Add(new TourRealizationDto(tourStart));
                }
            }
            return tourDto;
        }

        private void LoadFinishedTourStats()
        {
            foreach (var tourRealization in tourRealizationService.GetAllFinishedTours(SignInForm.curretnUserId))
            {
                FinishedToursStats.Add(GetTourStats(MakeFinishedTour(tourRealization)));
            }
        }

        private TourStatsDto GetTourStats(TourDto tour)
        {
            List<TourGuest> guests = tourGuestService.GetTourGuests(tour.TourStart.Id);
            int group1 = guests.FindAll(tg => tg.Years <= 18).Count();
            int group2 = guests.FindAll(tg => tg.Years > 18 && tg.Years <= 50).Count();
            int group3 = guests.FindAll(tg => tg.Years > 50).Count();
            return new TourStatsDto(tour, group1, group2, group3);
        }

        private TourDto MakeFinishedTour(TourRealization tourRealization)
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

        private TourDto MakeUpcomingTour(int tourId)
        {
            Tour tour = tourService.GetById(tourId);
            Location location = locationService.GetById(tour.LocationId);
            Language language = languageService.GetById(tour.LanguageId);
            TourDto tourDto = new TourDto(tour, location, language);
            if (imageService.GetFirstByEntityAndType(tourDto.Id, ResourceType.Tour) == null) tourDto.ImagePath = "";
            else tourDto.ImagePath = imageService.GetFirstByEntityAndType(tourDto.Id, ResourceType.Tour).Path;
            return tourDto;
        }

        private void ChangePageTitle(int index)
        {
            if (index == 0) PageTitle = "Upcoming Tours";
            else if (index == 1) PageTitle = "Finished Tours";
            else PageTitle = "Cancelled Tours";
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
