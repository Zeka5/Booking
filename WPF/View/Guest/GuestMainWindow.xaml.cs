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
using BookingApp.Dto;
using BookingApp.Repository;
using BookingApp.Observer;
using System.Reflection;
using System.ComponentModel;
using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModels;

namespace BookingApp.WPF.View.Guest
{
    public partial class GuestMainWindow : Window
    {
        public GuestMainWindowViewModel GuestMainWindowViewModel { get; set; }
        public GuestMainWindow(User user)
        {
            InitializeComponent();
            GuestMainWindowViewModel = new GuestMainWindowViewModel(user, MainFrame.NavigationService);
            DataContext = GuestMainWindowViewModel;
            MainFrame.Navigate(new MainPage(user,MainFrame.NavigationService));
        }
    }   
}
