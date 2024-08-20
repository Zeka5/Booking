using BookingApp.Domain.Model;
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
    /// Interaction logic for MainOwnerPage.xaml
    /// </summary>
    public partial class OwnerHomePage : Page
    {
        public OwnerHomePageViewModel OwnerHomePageViewModel { get; set; }
        public OwnerHomePage(User user)
        {
            InitializeComponent();
            OwnerHomePageViewModel = new OwnerHomePageViewModel(user);
            DataContext = OwnerHomePageViewModel;
        }

        private void RegisterAccommodation_Click(object sender, RoutedEventArgs e)
        {
            OwnerHomePageViewModel.RegisterAccommodation();
        }
    }
}
