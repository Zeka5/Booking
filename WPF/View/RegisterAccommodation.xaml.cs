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
using System.Windows.Shapes;

namespace BookingApp.WPF.View
{
    /// <summary>
    /// Interaction logic for RegisterAccommodation.xaml
    /// </summary>
    public partial class RegisterAccommodation : Window
    {
        //public RegisterAccommodationViewModel RegisterAccommodationViewModel { get; set; }

        public RegisterAccommodation(User user)
        {
            InitializeComponent();
            //RegisterAccommodationViewModel = new RegisterAccommodationViewModel(user);
            //DataContext = RegisterAccommodationViewModel;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            //RegisterAccommodationViewModel.AddClick();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            //if (!RegisterAccommodationViewModel.Submit()) MessageBox.Show("Inputs not valid, accommodation not registered.");
            //else Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
