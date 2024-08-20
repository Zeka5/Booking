using BookingApp.WPF.ViewModels.TourGuestViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.View.TourGuest
{
    /// <summary>
    /// Interaction logic for TourRequestPage.xaml
    /// </summary>
    public partial class TourRequestPage : Page
    {
        public TourRequestViewModel TourRequestViewModel { get; set; }
        public TourRequestPage()
        {
            InitializeComponent();
            TourRequestViewModel = new TourRequestViewModel();
            DataContext = TourRequestViewModel;
        }
        private void NextTourRequestTourGuestClick(object sender, RoutedEventArgs e)
        {
            TourRequestViewModel.NextTourRequestTourGuest();
        }
        private void ConfirmTourRequestClick(object sender, RoutedEventArgs e)
        {
            TourRequestViewModel.ConfirmTourRequest();
            NavigationService.Navigate(new Tours());
        }
        public void DecreaseNumberOfTourGuestsCLick(object sender, RoutedEventArgs e)
        {
            TourRequestViewModel.DecreaseNumberOfTourGuests();
        }
        public void IncreaseNumberOfTourGuestsCLick(object sender, RoutedEventArgs e)
        {
            TourRequestViewModel.IncreaseNumberOfTourGuests();
        }
    }
}
