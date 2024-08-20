using BookingApp.Domain.Model;
using BookingApp.Dto;
using BookingApp.Repository;
using BookingApp.WPF.ViewModels.TourGuestViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BookingApp.WPF.View
{
    /// <summary>
    /// Interaction logic for FinishedTours.xaml
    /// </summary>
    public partial class FinishedTours : Page
    {
        public FinishedToursViewModel FinishedToursViewModel { get; set; }
        public FinishedTours()
        {
            InitializeComponent();
            FinishedToursViewModel = new FinishedToursViewModel();
            DataContext = FinishedToursViewModel;
        }
        private void RateTourClick(object sender, RoutedEventArgs e)
        {
            if(FinishedToursViewModel.RateTour() != null)
            {
                NavigationService?.Navigate(new RateTour(FinishedToursViewModel.RateTour().TourId));
            }
        }
    }
}
