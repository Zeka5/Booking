using BookingApp.WPF.ViewModels;
using GalaSoft.MvvmLight.Views;
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
    /// Interaction logic for DriverHelpPage.xaml
    /// </summary>
    public partial class DriverHelpPage : Page
    {
        public DriverHelpPageViewModel ViewModel { get; set; }
        public int id;
        public NavigationService NavigationService { get; set; }
        public DriverHelpPage(string pageKey, int id, NavigationService navigationService)
        {
            InitializeComponent();
            this.id = id;
            this.NavigationService = navigationService;
            ViewModel = new DriverHelpPageViewModel(id, navigationService);
            DataContext = ViewModel;
            var helpContent = ViewModel.GetHelpContent(pageKey);
            HelpTitle.Text = helpContent.Title;
            HelpContent.Text = helpContent.Content;
            if (!string.IsNullOrEmpty(helpContent.ImagePath))
            {
                HelpImage.Source = new BitmapImage(new Uri(helpContent.ImagePath, UriKind.Relative));
            }
            else
            {
                HelpImage.Visibility = Visibility.Collapsed;
            }
        }

    }
}
