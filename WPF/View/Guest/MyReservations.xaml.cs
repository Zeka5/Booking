using BookingApp.Domain.Model;
using BookingApp.Dto;
using BookingApp.Observer;
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

namespace BookingApp.WPF.View.Guest
{
    /// <summary>
    /// Interaction logic for MyReservations.xaml
    /// </summary>
    public partial class MyReservations : Page 
    {
        public MyReservationsViewModel MyReservationsViewModel { get; set; }
        private User user;
        public MyReservations(User user , int index, NavigationService navigationService)
        {
            InitializeComponent();
            MyReservationsViewModel = new MyReservationsViewModel(user,navigationService);
            DataContext = MyReservationsViewModel;
            this.user = user;
            TabItemToShow(index);
        }
        private void TabItemToShow(int index)
        {
            if (index == 2) { MyReservationsTab.SelectedItem = RequestsTabItem; }
            else if (index == 1) { MyReservationsTab.SelectedItem = UpcomingTabItem; }
            else { MyReservationsTab.SelectedItem = PastTabItem; }
        }
    }
}
