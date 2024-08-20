using BookingApp.Domain.Model;
using BookingApp.Dto;
using BookingApp.Repository;
using BookingApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace BookingApp.WPF.View.Guest
{
    public partial class AccommodationView : Page
    {
        public AccommodationViewViewModel AccommodationViewViewModel { get; set; }
        public AccommodationDto AccommodationDto { get; set; }
        private User user;
        public AccommodationView(AccommodationDto accommodationDto , User user, NavigationService navigationService)
        {
            InitializeComponent();
            AccommodationViewViewModel = new AccommodationViewViewModel(accommodationDto,user, navigationService);
            DataContext = AccommodationViewViewModel;
            AccommodationDto = accommodationDto;
            this.user = user;         
        }
    }
}
