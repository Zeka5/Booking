using BookingApp.Domain.Model;
using BookingApp.Dto;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.WPF.ViewModels;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for VehicleRegistration.xaml
    /// </summary>
    public partial class VehicleRegistration : Page
    {
        public VehicleRegistrationViewModel ViewModel { get; set; }

        private int id;
        public NavigationService NavigationService { get; set; }
        public VehicleRegistration(int id, NavigationService navigationService)
        {
            InitializeComponent();
            ViewModel = new VehicleRegistrationViewModel(id, navigationService);
            DataContext = ViewModel;
            this.id = id;
            this.NavigationService = navigationService;
        }

        public void Register(object sender, RoutedEventArgs e)
        {
            bool check = ViewModel.Register();
            if(!check) { return; }
            this.NavigationService.Navigate(new DriverProfilePage(id, NavigationService));
        }

        public void AddImage(object sender, RoutedEventArgs e)
        {
            ViewModel.AddImage();
        }
    }
}
