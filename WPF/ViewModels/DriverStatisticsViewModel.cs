using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.View;
using BookingApp.WPF.View;
using GalaSoft.MvvmLight.Messaging;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using System.Windows.Media;
using System.IO;
using System.Windows.Media.Imaging;
using QuestPDF.Infrastructure;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using BookingApp.Utilities;
using BookingApp.WPF.View.DriverView;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels
{
    public class DriverStatisticsViewModel : INotifyPropertyChanged
    {
        private DriveService driveService;
        public ObservableCollection<DriveStatistics> DriveStatistics { get; set; }
        public SeriesCollection PriceChart { get; set; }
        private readonly ChartValues<double> priceChartData;
        public SeriesCollection DrivesChart { get; set; }
        private readonly ChartValues<int> drivesChartData;
        public SeriesCollection DurationChart { get; set; }
        private readonly ChartValues<int> durationChartData;
        public List<int> Years { get; set; }
        private int selectedChartYear;
        public int SelectedChartYear
        {
            get { return selectedChartYear; }
            set
            {
                if (selectedChartYear != value)
                {
                    selectedChartYear = value;
                    OnPropertyChanged();
                }
            }
        }
        private string averageYearlyPrice;
        public string AverageYearlyPrice
        {
            get { return averageYearlyPrice; }
            set
            {
                if (averageYearlyPrice != value)
                {
                    averageYearlyPrice = value;
                    OnPropertyChanged();
                }
            }
        }
        private string averageYearlyDrives;
        public string AverageYearlyDrives
        {
            get { return averageYearlyDrives; }
            set
            {
                if (averageYearlyDrives != value)
                {
                    averageYearlyDrives = value;
                    OnPropertyChanged();
                }
            }
        }
        private string averageYearlyDuration;
        public string AverageYearlyDuration
        {
            get { return averageYearlyDuration; }
            set
            {
                if (averageYearlyDuration != value)
                {
                    averageYearlyDuration = value;
                    OnPropertyChanged();
                }
            }
        }
        private List<string> xAxisLabels;
        public List<string> XAxisLabels
        {
            get { return xAxisLabels; }
            set
            {
                if (xAxisLabels != value)
                {
                    xAxisLabels = value;
                    OnPropertyChanged();
                }
            }
        }

        public MyICommand ShowMonthlyCommand { get; set; }
        public NavigationService NavigationService { get; set; }

        private readonly int id;
        private List<long> durations;

        public DriverStatisticsViewModel(int id, NavigationService navigationService)
        {
            driveService = new DriveService(Injector.CreateInstance<IDriveRepository>());

            priceChartData = new ChartValues<double>();
            PriceChart = new SeriesCollection() { new LineSeries() { Values = priceChartData } };

            drivesChartData = new ChartValues<int>();
            DrivesChart = new SeriesCollection() { new LineSeries() { Values = drivesChartData } };
            
            durationChartData = new ChartValues<int>();
            DurationChart = new SeriesCollection() { new LineSeries() { Values = durationChartData } };

            XAxisLabels = new List<string>();
            Years = new List<int>();
            DriveStatistics = new ObservableCollection<DriveStatistics>();

            ShowMonthlyCommand = new MyICommand(Execute_ShowMonthly);

            this.id = id;
            this.NavigationService = navigationService;
            durations = new List<long>();

            Update();
        }
        private void Execute_ShowMonthly()
        {
            if (SelectedChartYear == 0)
            {
                MessageBox.Show("Please choose one of the following years.", "No celected item", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            NavigationService.Navigate(new DriverMonthlyStatistics(id, SelectedChartYear));
        }

        private void Update()
        {
            CountStatisticsForYear();
            CountAveragePriceAndDrives();
            CountAverageDuration();
        }
        public BitmapSource RenderControlToBitmap(UIElement control, int width, int height, double dpiX, double dpiY, Brush backgroundBrush)
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                drawingContext.DrawRectangle(backgroundBrush, null, new Rect(0, 0, width, height));
            }

            RenderTargetBitmap renderTarget = new RenderTargetBitmap(
                (int)(width * dpiX / 96), (int)(height * dpiY / 96), dpiX, dpiY, PixelFormats.Pbgra32);
            renderTarget.Render(drawingVisual);

            control.Measure(new System.Windows.Size(width, height));
            control.Arrange(new Rect(new System.Windows.Size(width, height)));
            renderTarget.Render(control);

            return renderTarget;
        }

        public byte[] BitmapSourceToByteArray(BitmapSource bitmapSource)
        {
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
            using (MemoryStream stream = new MemoryStream())
            {
                encoder.Save(stream);
                return stream.ToArray();
            }
        }

        
        public void Header(QuestPDF.Infrastructure.IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem().Text(text =>
                {
                    text.Span("Yearly Statistics").Bold().FontSize(16).FontColor(QuestPDF.Helpers.Colors.Blue.Medium);
                });

                row.ConstantItem(100).Text(DateTime.Now.ToString("MMMM dd, yyyy")).FontSize(10).AlignRight();
            });
        }

        public void Content(QuestPDF.Infrastructure.IContainer container, byte[] priceChartImage, byte[] drivesChartImage, byte[] durationChartImage)
        {

            double averagePrice = 120.22;
            int averageDrives = 3;
            int averageDuration = 200;
            container.Column(column =>
            {
                column.Spacing(20);

                column.Item().Text(" ").FontSize(20);

                // Price Statistics Section
                column.Item().Text("\n\nPrice Statistics").Bold().FontSize(20);
                column.Item().Text($"Average Drive Price: {averagePrice:C}").FontSize(16);
                column.Item().Image(priceChartImage);

                // Number of Drives Statistics Section
                column.Item().Text("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\nNumber of Drives Statistics").Bold().FontSize(20);
                column.Item().Text($"Average Number of Drives: {averageDrives}").FontSize(16);
                column.Item().Image(drivesChartImage);

                // Duration Statistics Section
                column.Item().Text("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\nDuration Statistics").Bold().FontSize(20);
                column.Item().Text($"Average Drive Duration: {averageDuration} minutes").FontSize(16);
                column.Item().Image(durationChartImage);
            });
        }

        public void Footer(QuestPDF.Infrastructure.IContainer container)
        {
            container.AlignCenter().Text(x =>
            {
                x.CurrentPageNumber();
                x.Span(" / ");
                x.TotalPages();
            });
        }

        private void CountAverageDuration()
        {
            TimeSpan totalAverageDuration = TimeSpan.FromTicks(durations.Sum() / durations.Count());

            int hours = totalAverageDuration.Hours;
            int minutes = totalAverageDuration.Minutes;

            AverageYearlyDuration = $"{hours:D2}h {minutes:D2}m";
        }

        private void CountAveragePriceAndDrives()
        {
            double totalPrice = priceChartData.Sum();
            
            double totalAveragePrice = totalPrice / priceChartData.Count;
            AverageYearlyPrice = totalAveragePrice.ToString("F2");
            AverageYearlyDrives = priceChartData.Count.ToString("F2");
        }

        private void CountStatisticsForYear()
        {
            for (int i = driveService.GetMinYear(); i <= DateTime.Now.Year; i++)
            {
                if (driveService.GetByDriverAndYear(id, i).Count() == 0) { continue; }
                XAxisLabels.Add(i.ToString());
                Years.Add(i);
                GetStatisticsForYear(i);
            }
        }

        private void GetStatisticsForYear(int i)
        {
            int numberOfDrives = 0;
            double totalPrice = 0;
            TimeSpan totalDuration = TimeSpan.Zero;
            foreach (Drive drive in driveService.GetByDriverAndYear(id, i))
            {
                numberOfDrives++;
                totalPrice += drive.EndPrice;
                totalDuration += (drive.EndTime - drive.DepartureTime);
            }

            string averageDuration = GetAverageDuration(totalDuration, numberOfDrives);
            double averagePrice = totalPrice / numberOfDrives;
            priceChartData.Add(averagePrice);
            drivesChartData.Add(numberOfDrives);
            DriveStatistics.Add(new DriveStatistics(i, numberOfDrives, averageDuration, averagePrice));
        }

        private string GetAverageDuration(TimeSpan totalDuration, int numberOfDrives)
        {
            long averageTicks = totalDuration.Ticks / numberOfDrives;
            durations.Add(averageTicks);

            TimeSpan averageDuration = TimeSpan.FromTicks(averageTicks);
            durationChartData.Add((int)averageDuration.TotalMinutes);

            int hours = averageDuration.Hours;
            int minutes = averageDuration.Minutes;
            string formattedDuration = $"{hours:D2}h {minutes:D2}m";
            return formattedDuration;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
