using BookingApp.Domain.Model;
using BookingApp.WPF.View.DriverView;
using BookingApp.WPF.View.Guest;
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
using System.Windows.Shapes;

namespace BookingApp.WPF.View
{
    /// <summary>
    /// Interaction logic for DriverMainWindow.xaml
    /// </summary>
    public partial class DriverMainWindow : Window
    {
        public DriverHomePageViewModel ViewModel { get; set; }
        public DriverMainWindow(int id)
        {
            InitializeComponent();
            ViewModel = new DriverHomePageViewModel(id, MainFrame.NavigationService);
            DataContext = ViewModel;
            MainFrame.Navigate(new DriverHomePage(id, MainFrame.NavigationService));
        }
    }
}
