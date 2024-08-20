using BookingApp.Domain.Model;
using BookingApp.Dto;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.WPF.View.TourGuest;
using BookingApp.WPF.ViewModels.TourGuestViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
    /// Interaction logic for TouristHomePage.xaml
    /// </summary>
    public partial class TouristHomePage : Window, INotifyPropertyChanged
    {
        public static bool IsFastDrive { get; set; }
        public TourRequestViewModel TourRequestViewModel { get; set; }
        public TouristHomePage()
        {
            InitializeComponent();
            DataContext = this;
            IsFastDrive = false;
            TourRequestViewModel = new TourRequestViewModel();

            TourRequestViewModel.SetExpiredTourRequests();
            DeleteExpiredVouchers();
        }
        private string profileImagePath;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }

        }
        private void DeleteExpiredVouchers()
        {
            VoucherRepository voucherRepository = new VoucherRepository();
            foreach(var voucher in voucherRepository.GetByUserId(SignInForm.curretnUserId))
            {
                if(voucher.ValidityEnd <= DateTime.Now)
                {
                    voucherRepository.Delete(voucher);
                }
            }
        }
        private void DrivesClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new MyDriveReservations();
        }
        private void ToursClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new Tours();
        }
        private void ActiveToursClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new ActiveTours();
        }
        private void FinishedToursClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new FinishedTours(); 
        }
        private void NotificationClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new TourGuestNotifications();
        }
        private void TourRequestClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new MyTourRequestsPage();
        }
    }
}
