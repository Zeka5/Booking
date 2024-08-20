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

namespace BookingApp.WPF.View.DriverView
{
    /// <summary>
    /// Interaction logic for BreakRequest.xaml
    /// </summary>
    public partial class BreakRequest : Page
    {
        public BreakRequestViewModel ViewModel { get; set; }
        private int id;
        public NavigationService NavigationService { get; set; }
        public BreakRequest(int id, NavigationService navigationService)
        {
            InitializeComponent();
            ViewModel = new BreakRequestViewModel(SignInForm.curretnUserId);
            DataContext = ViewModel;
            this.id = id;
            NavigationService = navigationService;
        }

        public void SendRequest(object sender, RoutedEventArgs e)
        {
            bool check = ViewModel.RequestBreak();
            if (!check) { return; }
            this.NavigationService.Navigate(new DriverProfilePage(id, NavigationService));
        }
    }
}
