using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Services;
using BookingApp.Utilities;
using BookingApp.WPF.View;
using BookingApp.WPF.View.Guide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.GuideViewModels
{
    public class ProfilePageViewModel
    {
        public NavigationService navService {  get; set; }
        private GuideService guideService;
        private TourRealizationService tourRealizationService;
        private GuideQuitJobService quitJobService;
        private SuperGuideService superGuideService;
        public GuideDto Guide { get; set; }
        public MyICommand QuitJobCommand {  get; set; }
        public MyICommand SignOutCommand { get; set; }
        public ProfilePageViewModel(NavigationService navService) 
        { 
            this.navService = navService;
            guideService = new GuideService(Injector.CreateInstance<IGuideRepository>());
            QuitJobCommand = new MyICommand(Execute_QuitJobCommand);
            SignOutCommand = new MyICommand(Execute_SignOutCommand);
            tourRealizationService = new TourRealizationService(Injector.CreateInstance<ITourRealizationRepository>(), new TourService(Injector.CreateInstance<ITourRepository>()), new LocationService(Injector.CreateInstance<ILocationRepository>()), new LanguageService(Injector.CreateInstance<ILanguageRepository>()), new TourReservationService(Injector.CreateInstance<ITourReservationRepository>()), new CheckPointService(Injector.CreateInstance<ICheckPointRepository>()));
            quitJobService = new GuideQuitJobService(tourRealizationService, new VoucherService(Injector.CreateInstance<IVoucherRepository>(), new TourReservationService(Injector.CreateInstance<ITourReservationRepository>())));
            superGuideService = new SuperGuideService(guideService, tourRealizationService, new TourRatingService(Injector.CreateInstance<ITourRatingRepository>(), new TourGuestService(Injector.CreateInstance<ITourGuestRepository>(), new TourReservationService(Injector.CreateInstance<ITourReservationRepository>()))),new TourService(Injector.CreateInstance<ITourRepository>()));
            LoadGuide();
        }

        private void LoadGuide()
        {

            Guide guide = guideService.GetByUserId(SignInForm.curretnUserId);
            if (!guide.IsSuperGuide)
            {
                 superGuideService.IsSuperGuide(SignInForm.curretnUserId);
            }
            else if(guide.IsSuperGuide && guide.SuperGuideStartDate.AddMonths(12)>=DateOnly.FromDateTime(DateTime.Now.Date))
            {
                superGuideService.IsSuperGuide(SignInForm.curretnUserId);
            }
            Guide=new GuideDto(guideService.GetByUserId(SignInForm.curretnUserId));
        }

        private void Execute_QuitJobCommand()
        {
            MessageBoxResult result = MessageBox.Show("If you click Yes then you quit your job and all your upcoming tours will be cancelled!", "Quiting Job", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                quitJobService.Quit(SignInForm.curretnUserId);
            }
            return;
        }
        private void Execute_SignOutCommand()
        {
            throw new NotImplementedException();
        }
    }
}
