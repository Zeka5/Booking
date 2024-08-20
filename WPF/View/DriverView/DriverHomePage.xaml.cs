using BookingApp.Domain.Model;
using BookingApp.Dto;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.View.DriverView
{
    /// <summary>
    /// Interaction logic for DriverHomePage.xaml
    /// </summary>
    public partial class DriverHomePage : Page
    {
        public DriverHomePageViewModel ViewModel { get; set; }
        public DriverHomePage(int id, NavigationService navigationService)
        {
            InitializeComponent();
            ViewModel = new DriverHomePageViewModel(id, navigationService);
            if (ViewModel.IsFirstLogIn())
            {
                var wizard = new WizardWindow();
                wizard.Show();
                ViewModel.SetSecondLogIn();
            }
            DataContext = ViewModel;
        }
    }
}
