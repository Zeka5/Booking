using BookingApp.Domain.Model;
using BookingApp.Dto;
using BookingApp.Repository;
using BookingApp.View.Validation;
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
    public partial class Reservation : Page 
    {
        public ReservationViewModel ReservationViewModel { get; set; }
        private User user;
        public Reservation(AccommodationDto accommodationDto, User user, NavigationService navigationService)
        {
            InitializeComponent();
            ReservationViewModel = new ReservationViewModel(accommodationDto,user,navigationService);
            DataContext = ReservationViewModel;  
            this.user = user;
            DaysToStayValidation.Min = accommodationDto.MinStayDays;
            GuestNumberValidation.Max = accommodationDto.MaxGuest;
        }
    }
}
