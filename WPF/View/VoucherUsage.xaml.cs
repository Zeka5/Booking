using BookingApp.Domain.Model;
using BookingApp.Dto;
using BookingApp.Repository;
using BookingApp.WPF.ViewModels.TourGuestViewModels;
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
    /// Interaction logic for VoucherUsage.xaml
    /// </summary>
    public partial class VoucherUsage : Page
    {
        public VoucherUsageViewModel VoucherUsageViewModel { get; set; }
        public VoucherUsage(int tourRealizationId, BookingApp.WPF.View.TourRealizationPage previousPage)
        {
            InitializeComponent();
            VoucherUsageViewModel = new VoucherUsageViewModel(tourRealizationId, previousPage);
            DataContext = VoucherUsageViewModel; 
        }
        private void UseVaucherClick(object sender, RoutedEventArgs e)
        {
            VoucherUsageViewModel.UseVaucher();
        }
    }
}
