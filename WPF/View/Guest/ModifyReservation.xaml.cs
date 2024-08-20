using BookingApp.Domain.Model;
using BookingApp.Dto;
using BookingApp.Repository;
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

namespace BookingApp.WPF.View.Guest
{
    /// <summary>
    /// Interaction logic for ModifyReservation.xaml
    /// </summary>
    public partial class ModifyReservation : Page
    {
        public ModifyReservationViewModel ModifyReservationViewModel { get; set; }
        public ModifyReservation(User user,NavigationService navigationService ,int accommodationReservationId)
        {
            InitializeComponent();
            ModifyReservationViewModel = new ModifyReservationViewModel(user,navigationService,accommodationReservationId);
            DataContext = ModifyReservationViewModel;     
        }
    }
}
