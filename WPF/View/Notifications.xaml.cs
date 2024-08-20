using BookingApp.Dto;
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

namespace BookingApp.WPF.View
{
    /// <summary>
    /// Interaction logic for Notifications.xaml
    /// </summary>
    public partial class Notifications : Page
    {
        public Frame Frame { get; set; }
        public NotificationsViewModel NotificationsViewModel { get; set; }

        public Notifications(int ownerId)
        {
            InitializeComponent();
            NotificationsViewModel = new NotificationsViewModel(ownerId);
            DataContext = NotificationsViewModel;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Frame.Visibility = Visibility.Collapsed;
        }

        private void Rate_Click(object sender, RoutedEventArgs e)
        {
            NotificationsViewModel.Rate(sender);
        }
    }
}
