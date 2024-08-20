using BookingApp.Dto;
using BookingApp.Domain.Model;
using BookingApp.Repository;
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
using BookingApp.WPF.ViewModels;

namespace BookingApp.WPF.View
{
    /// <summary>
    /// Interaction logic for RateGuest.xaml
    /// </summary>
    public partial class RateGuest : Window
    {
        public RateGuestViewModel RateGuestViewModel { get; set; }

        public RateGuest(RatingNotificationDto notificationDto)
        {
            InitializeComponent();
            RateGuestViewModel = new RateGuestViewModel(notificationDto);
            DataContext = RateGuestViewModel;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (!RateGuestViewModel.Submit()) MessageBox.Show("Inputs not valid, guest not rated.");
            else Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
