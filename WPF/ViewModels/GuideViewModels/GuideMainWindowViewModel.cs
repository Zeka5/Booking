using BookingApp.Utilities;
using BookingApp.WPF.View.Guide;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.GuideViewModels
{
    public class GuideMainWindowViewModel
    {
        public NavigationService NavService {get;set;}

        public MyICommand NavigateToHomePage { get; set; }
        public MyICommand NavigateToReservedToursPage { get; set; }
        public MyICommand NavigateToTourReviewsPage { get; set; }
        public MyICommand NavigateToTourRequestsPage { get; set; }
        public MyICommand NavigateToHelpPage { get; set; }
        public MyICommand NavigateToGuideProfilePage { get; set; }

        public GuideMainWindowViewModel(NavigationService navService) {
            NavService = navService;
            navService.Navigate(new HomePage(NavService));
            NavigateToHomePage = new MyICommand(Execute_NavigateToHomePage);
            NavigateToReservedToursPage = new MyICommand(Execute_NavigateToReservedToursPage);
            NavigateToTourReviewsPage = new MyICommand(Execute_NavigateToTourReviewsPage);
            NavigateToTourRequestsPage = new MyICommand(Execute_NavigationToTourRequestPage);
            NavigateToGuideProfilePage = new MyICommand(Execute_NavigateToGuideProfilePage);
        }

        private void Execute_NavigationToTourRequestPage()
        {
            NavService.Navigate(new RequestsPage(NavService));
        }

        private void Execute_NavigateToTourReviewsPage()
        {
            NavService.Navigate(new TourReviewsPage(NavService));
        }

        private void Execute_NavigateToReservedToursPage()
        {
            NavService.Navigate(new ReservedToursPage(NavService));
        }

        private void Execute_NavigateToHomePage()
        {
            NavService.Navigate(new HomePage(NavService));
        }
        private void Execute_NavigateToGuideProfilePage()
        {
            NavService.Navigate(new ProfilePage(NavService));
        }
    }
}
