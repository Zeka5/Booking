using BookingApp.Dto;
using BookingApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Diagnostics;
using System.IO.Enumeration;
using System.Reflection.PortableExecutable;

namespace BookingApp.WPF.View.DriverView
{
    /// <summary>
    /// Interaction logic for DriverStatistics.xaml
    /// </summary>
    public partial class DriverStatistics : Page
    {
        public DriverStatisticsViewModel ViewModel { get; set; }

        public DriverStatistics(int id, NavigationService navigationService)
        {
            InitializeComponent();
            ViewModel = new DriverStatisticsViewModel(id, navigationService);
            DataContext = ViewModel;
        }

        private void DownloadPdf(object sender, RoutedEventArgs e)
        {
            int height = 800;
            int width = 600;
            var whiteBackground = new SolidColorBrush(System.Windows.Media.Colors.White);
            var priceChartBitmap = ViewModel.RenderControlToBitmap(PriceChartControl, (int)DurationChartControl.ActualWidth,
                (int)DurationChartControl.ActualHeight, 300, 300, whiteBackground);
            var drivesChartBitmap = ViewModel.RenderControlToBitmap(DrivesChartControl, (int)DurationChartControl.ActualWidth,
                (int)DurationChartControl.ActualHeight, 300, 300, whiteBackground);
            var durationChartBitmap = ViewModel.RenderControlToBitmap(DurationChartControl, (int)DurationChartControl.ActualWidth,
                (int)DurationChartControl.ActualHeight, 300, 300, whiteBackground);

            // konvertujemo bitmap odnosno dobijenu sliku u niz bajtova
            byte[] priceChartImage = ViewModel.BitmapSourceToByteArray(priceChartBitmap);
            byte[] drivesChartImage = ViewModel.BitmapSourceToByteArray(drivesChartBitmap);
            byte[] durationChartImage = ViewModel.BitmapSourceToByteArray(durationChartBitmap);

            //potrebna dozvola za opensource paket
            QuestPDF.Settings.License = LicenseType.Community;
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);
                    page.Size(PageSizes.A4);

                    page.Header().Element(ViewModel.Header);
                    page.Content().Element(c => ViewModel.Content(c, priceChartImage, drivesChartImage, durationChartImage));
                    page.Footer().Element(ViewModel.Footer);
                });
            })
            .GeneratePdf("statistics_report.pdf");
            try
            {
                string fileName = "statistics_report.pdf";
                Process.Start(new ProcessStartInfo(fileName) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unsuccessful download of PDF: " + ex.Message);
            }

            MessageBox.Show("PDF downloaded succesfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

        }

    }
}
