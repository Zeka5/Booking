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
    /// Interaction logic for MyDriveReservations.xaml
    /// </summary>
    public partial class MyDriveReservations : Page
    {
        public MyDriveReservationsViewModel ViewModel { get; set; }
        public MyDriveReservations()
        {
            ViewModel = new MyDriveReservationsViewModel();
            InitializeComponent();
            DataContext = ViewModel;
        }
        public void NewDriveReservationClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ReserveDrivePage());
        }
        public void TourGuestIsLateClick(object sender, RoutedEventArgs e)
        {
            ViewModel.TourGuestIsLate();
        }
        public void UnreliableDriverClick(object sender, RoutedEventArgs e)
        {
            ViewModel.UnreliableDriver();
        }
    }
}
