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
    /// Interaction logic for MyTourRequestsPage.xaml
    /// </summary>
    public partial class MyTourRequestsPage : Page
    {
        public MyTourRequestsViewModel MyTourRequestsViewModel { get; set; }
        public MyTourRequestsPage()
        {
            InitializeComponent();
            MyTourRequestsViewModel = new MyTourRequestsViewModel();
            DataContext = MyTourRequestsViewModel;
        }
        public void NewTourRequestClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TourRequestPage());
        }
        public void StatisticsClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TourRequestStatistics());
        }
    }
}
