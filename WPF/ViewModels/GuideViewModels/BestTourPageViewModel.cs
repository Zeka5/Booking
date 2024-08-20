using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Services;
using BookingApp.Utilities;
using BookingApp.WPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.GuideViewModels
{
    public class BestTourPageViewModel
    {
        public NavigationService NavServce { get; set; }

        public MyICommand YearChangedCommand {  get; set; }
        public List<string> Years {  get; set; }
        public string SelectedYear {  get; set; }

        public ObservableCollection<TourStatsDto> BestToursStats {  get; set; }
        private TourRealizationService tourRealizationService;
        private TourGuestService tourGuestService;
        private TourService tourService;
        private LocationService locationService;
        private LanguageService languageService;
        private ImageService imageService;

        private int maxNumberOfGuests;
        public BestTourPageViewModel(NavigationService navService) {
            NavServce = navService;
            YearChangedCommand = new MyICommand(Execute_YearChangedCommand);
            Years = new List<string>();
            SelectedYear = string.Empty;
            BestToursStats = new ObservableCollection<TourStatsDto>();
            tourService = new TourService(Injector.CreateInstance<ITourRepository>());
            locationService = new LocationService(Injector.CreateInstance<ILocationRepository>());
            languageService = new LanguageService(Injector.CreateInstance<ILanguageRepository>());
            imageService = new ImageService(Injector.CreateInstance<IImageRepository>());
            tourRealizationService =new TourRealizationService(Injector.CreateInstance<ITourRealizationRepository>(),tourService,locationService,languageService,new TourReservationService(Injector.CreateInstance<ITourReservationRepository>()),new CheckPointService(Injector.CreateInstance<ICheckPointRepository>()));
            tourGuestService=new TourGuestService(Injector.CreateInstance<ITourGuestRepository>(),new TourReservationService(Injector.CreateInstance<ITourReservationRepository>()));
            maxNumberOfGuests = 0;
            LoadYears();
        }

        private void Execute_YearChangedCommand()
        {
            FindMaxNumberOfTourists();
        }

        private void LoadYears()
        {
            Years.Add("All time");
            int currentYear = DateTime.Now.Year;  // Dobijanje trenutne godine
            for (int year = currentYear; year >= 2000; year--)
            {
                Years.Add(year.ToString());
            }
        }
        public void FindMaxNumberOfTourists()
        {
            maxNumberOfGuests = 0;
            foreach (var tourRealization in tourRealizationService.GetAllFinishedTours(SignInForm.curretnUserId,SelectedYear))
            {
                if (maxNumberOfGuests < tourGuestService.GetTourGuests(tourRealization.Id).Count)
                {
                    maxNumberOfGuests = tourGuestService.GetTourGuests(tourRealization.Id).Count;
                }
            }
            LoadBestTourStats();
        }

        private void LoadBestTourStats() { 
            BestToursStats.Clear();
            foreach(var tourRealization in tourRealizationService.GetAllFinishedTours(SignInForm.curretnUserId,SelectedYear))
            {
                if(tourGuestService.GetTourGuests(tourRealization.Id).Count==maxNumberOfGuests)
                BestToursStats.Add(GetTourStats(MakeTourDto(tourRealization)));
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

        private TourDto MakeTourDto(TourRealization tourRealization)
        {
            Tour tour = tourService.GetById(tourRealization.TourId);
            Location location = locationService.GetById(tour.LocationId);
            Language language = languageService.GetById(tour.LanguageId);
            TourDto tourDto = new TourDto(tour, location, language);
            if (imageService.GetFirstByEntityAndType(tourDto.Id, ResourceType.Tour) == null) tourDto.ImagePath = "";
            else tourDto.ImagePath = imageService.GetFirstByEntityAndType(tourDto.Id, ResourceType.Tour).Path;
            tourDto.TourStart = new TourRealizationDto(tourRealization);
            return tourDto;
        }

    }
}
