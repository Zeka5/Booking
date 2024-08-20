using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModels;
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

namespace BookingApp.WPF.View.DriverView
{
    /// <summary>
    /// Interaction logic for DriverProfilePage.xaml
    /// </summary>
    public partial class DriverProfilePage : Page
    {
        public DriverProfilePageViewModel ViewModel { get; set; }

        public DriverProfilePage(int id, NavigationService navigationService)
        {
            InitializeComponent();
            ViewModel = new DriverProfilePageViewModel(id, navigationService);
            DataContext = ViewModel;
        }

    }
}
