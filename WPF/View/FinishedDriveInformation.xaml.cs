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

namespace BookingApp.View.Guide
{
    /// <summary>
    /// Interaction logic for FinishedDriveInformation.xaml
    /// </summary>
    public partial class FinishedDriveInformation : Window
    {
        public FinishedDriveInformation(int Price)
        {
            InitializeComponent();
            DataContext = this;
            
            FinishTime.Content = "Time: " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + "\n\nPrice: " + Price;
        }

        public void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
