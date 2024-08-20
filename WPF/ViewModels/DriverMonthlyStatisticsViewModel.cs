using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.WPF.View;
using LiveCharts;
using LiveCharts.Wpf;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace BookingApp.WPF.ViewModels
{
    public class DriverMonthlyStatisticsViewModel : INotifyPropertyChanged
    {
        private DriverMonthlyStatisticsService monthlyStatisticsService;
        public ObservableCollection<DriveStatistics> DriveStatistics { get; set; }
        public SeriesCollection PriceChart { get; set; }
        private readonly ChartValues<double> priceChartData;
        public SeriesCollection DrivesChart { get; set; }
        private readonly ChartValues<int> drivesChartData;
        public SeriesCollection DurationChart { get; set; }
        private readonly ChartValues<int> durationChartData;
        public List<int> Years { get; set; }
        private int selectedChartMonth;
        public int SelectedChartMonth
        {
            get { return selectedChartMonth; }
            set
            {
                if (selectedChartMonth != value)
                {
                    selectedChartMonth = value;
                    OnPropertyChanged();
                }
            }
        }
        private string averageMonthlyPrice;
        public string AverageMonthlyPrice
        {
            get { return averageMonthlyPrice; }
            set
            {
                if (averageMonthlyPrice != value)
                {
                    averageMonthlyPrice = value;
                    OnPropertyChanged();
                }
            }
        }
        private string averageMonthlyDrives;
        public string AverageMonthlyDrives
        {
            get { return averageMonthlyDrives; }
            set
            {
                if (averageMonthlyDrives != value)
                {
                    averageMonthlyDrives = value;
                    OnPropertyChanged();
                }
            }
        }
        private string averageMonthlyDuration;
        public string AverageMonthlyDuration
        {
            get { return averageMonthlyDuration; }
            set
            {
                if (averageMonthlyDuration != value)
                {
                    averageMonthlyDuration = value;
                    OnPropertyChanged();
                }
            }
        }
        private readonly int id, year;
        private List<long> durations;

        public DriverMonthlyStatisticsViewModel(int id, int year)
        {
            monthlyStatisticsService = new DriverMonthlyStatisticsService();
            DriveStatistics = new ObservableCollection<DriveStatistics>();

            priceChartData = new ChartValues<double>();
            PriceChart = new SeriesCollection() { new LineSeries() { Values = priceChartData } };

            drivesChartData = new ChartValues<int>();
            DrivesChart = new SeriesCollection() { new LineSeries() { Values = drivesChartData } };

            durationChartData = new ChartValues<int>();
            DurationChart = new SeriesCollection() { new LineSeries() { Values = durationChartData } };

            this.id = id;
            this.year = year;
            durations = new List<long>();

            Update();
        }

        private void Update()
        {
            for (int i = 1; i < 13; i++)
            {
                if (monthlyStatisticsService.GetByMonthAndYear(id, i, year).Count() == 0) { continue; }
                GetStatisticsForMonth(i);
            }
            CountAverageDuration();
            CountAveragePriceAndDrives();
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
                    text.Span("Monthly Statistics").Bold().FontSize(16).FontColor(QuestPDF.Helpers.Colors.Blue.Medium);
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
                column.Item().Text("\n\n\n\n\n\n").Bold().FontSize(20);
                column.Item().Text("\n\n\n\n\n\n\n\n\n\n\n\nNumber of Drives Statistics").Bold().FontSize(20);
                column.Item().Text($"Average Number of Drives: {averageDrives}").FontSize(16);
                column.Item().Image(drivesChartImage);

                // Duration Statistics Section
                column.Item().Text("\n\n\n\n\n\n").Bold().FontSize(20);
                column.Item().Text("\n\n\n\n\n\n\n\n\n\n\n\nDuration Statistics").Bold().FontSize(20);
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

            AverageMonthlyDuration = $"{hours:D2}h {minutes:D2}m";
        }

        private void CountAveragePriceAndDrives()
        {
            double totalPrice = priceChartData.Sum();

            double totalAveragePrice = totalPrice / priceChartData.Count;
            AverageMonthlyPrice = totalAveragePrice.ToString("F2");
            AverageMonthlyDrives = priceChartData.Count.ToString("F2");
        }

        private void GetStatisticsForMonth(int i)
        {
            int numberOfDrives = 0;
            double totalPrice = 0;
            TimeSpan totalDuration = TimeSpan.Zero;
            foreach (Drive drive in monthlyStatisticsService.GetByMonthAndYear(id, i, year))
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
