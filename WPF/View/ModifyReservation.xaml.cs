using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModels.OwnerViewModels;
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
    /// Interaction logic for ModifyReservation.xaml
    /// </summary>
    public partial class ModifyReservation : Page
    {
        public ModifyReservationsViewModel ModifyReservationViewModel { get; set; }
        public ModifyReservation(int ownerId)
        {
            InitializeComponent();
            ModifyReservationViewModel = new ModifyReservationsViewModel(ownerId);
            DataContext = ModifyReservationViewModel;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            //ModifyReservationViewModel.Accept(sender);
        }

        private void Reject_Click(object sender, RoutedEventArgs e)
        {
            //ModifyReservationViewModel.Decline(sender);
        }
    }
}
