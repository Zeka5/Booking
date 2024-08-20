using BookingApp.Domain.Model;
using BookingApp.Dto;
using BookingApp.Repository;
using BookingApp.WPF.ViewModels;
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
using Xceed.Wpf.Toolkit;

namespace BookingApp.WPF.View.Guest
{

    public partial class RateOwnerPage : Page
    {
        public RateOwnerPageViewModel RateOwnerPageViewModel { get; set; }      
        private User user;

        public RateOwnerPage(User user ,NavigationService navigationService, int accommodationReservationId , int accommodationId)
        {
            InitializeComponent();
            RateOwnerPageViewModel = new RateOwnerPageViewModel(user,navigationService,accommodationReservationId, accommodationId);
            DataContext = RateOwnerPageViewModel;
            this.user = user;       
        }
    }
}
