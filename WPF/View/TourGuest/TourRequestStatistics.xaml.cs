using BookingApp.WPF.ViewModels.TourGuestViewModels;
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

namespace BookingApp.WPF.View.TourGuest
{
    /// <summary>
    /// Interaction logic for TourRequestStatistics.xaml
    /// </summary>
    public partial class TourRequestStatistics : Page
    {
        public TourRequestStatisticsViewModel TourRequestStatisticsViewModel { get; set; }
        public TourRequestStatistics()
        {
            InitializeComponent();
            TourRequestStatisticsViewModel = new TourRequestStatisticsViewModel();
            DataContext = TourRequestStatisticsViewModel;
        }
        public void ComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TourRequestStatisticsViewModel.ComboBoxSelectionChanged();
        }
    }
}
