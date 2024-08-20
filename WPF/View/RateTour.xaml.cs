using BookingApp.Dto;
using BookingApp.Repository;
using BookingApp.Domain.Model;
using Microsoft.Win32;
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
using Image = BookingApp.Domain.Model.Image;
using System.ComponentModel;
using BookingApp.Services;
using BookingApp.WPF.ViewModels.TourGuestViewModels;
using BookingApp.WPF.ViewModels.GuideViewModels;

namespace BookingApp.WPF.View
{
    /// <summary>
    /// Interaction logic for RateTour.xaml
    /// </summary>
    public partial class RateTour : Page
    {
        public RateTourViewModel RateTourViewModel { get; set; }    
        public RateTour(int selectedTourRealizationId)
        {
            InitializeComponent();
            RateTourViewModel = new RateTourViewModel(selectedTourRealizationId);
            DataContext = RateTourViewModel;

        }
        private void NextTourGuestClick(object sender, RoutedEventArgs e)
        {
            if(RateTourViewModel.NextTourGuest())
            {
                commentTextBox.Text = string.Empty;
                //ratingComboBox.SelectedIndex = 0;
                return;
            }
            NavigationService?.Navigate(new FinishedTours());
        }
        private void AddPictureClick(object sender, RoutedEventArgs e)
        {
            if (RateTourViewModel.AddPicture())
            {
                PicturesListBox.Background = null;
                return;
            }
                MessageBox.Show("You didn't pick any picture or unused pictures doesnt'exist!");
        }
        private void FirstStarClick(object sender, RoutedEventArgs e)
        {
            RateTourViewModel.FirstStarClick();
        }
        private void SecondStarClick(object sender, RoutedEventArgs e)
        {
            RateTourViewModel.SecondStarClick();
        }
        private void ThirdStarClick(object sender, RoutedEventArgs e)
        {
            RateTourViewModel.ThirdStarClick();
        }
        private void FourthStarClick(object sender, RoutedEventArgs e)
        {
            RateTourViewModel.FourthStarClick();
        }
        private void FifthStarClick(object sender, RoutedEventArgs e)
        {
            RateTourViewModel.FifthStarClick();
        }
    }
}