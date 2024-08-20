using BookingApp.Domain.Model;
using BookingApp.Dto;
using BookingApp.Repository;
using BookingApp.Stores;
using BookingApp.WPF.ViewModels;
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
using System.Windows.Shapes;

namespace BookingApp.WPF.View.OwnerViews
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        private User user;
        private Notifications notifications;

        public MainView(User user)
        {
            InitializeComponent();
            this.user = user;

            NavigationStore navigationStore = new NavigationStore();
            navigationStore.CurrentViewModel = new HomeViewModel(navigationStore, user.Id);
            DataContext = new MainViewModel(navigationStore, user.Id);

            notifications = new Notifications(user.Id);
            notifications.Frame = notificationsFrame;
            notificationsFrame.Content = notifications;
        }

        private void Notifications_Click(object sender, RoutedEventArgs e)
        {
            notificationsFrame.Visibility = Visibility.Visible;
        }
    }
}
