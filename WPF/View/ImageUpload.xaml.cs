using BookingApp.Dto;
using BookingApp.WPF.ViewModels;
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
using System.Windows.Shapes;

namespace BookingApp.WPF.View
{
    /// <summary>
    /// Interaction logic for ImageUpload.xaml
    /// </summary>
    public partial class ImageUpload : Window
    {
        public ImageUploadViewModel ImageUploadViewModel { get; set; }
        public ImageUpload(ObservableCollection<ImageItemDto> addedImages)
        {
            InitializeComponent();
            ImageUploadViewModel = new ImageUploadViewModel(addedImages);
            DataContext = ImageUploadViewModel;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            var selectedImage = button.DataContext as ImageItemDto;
            if (!ImageUploadViewModel.AddClick(selectedImage)) MessageBox.Show("Image already added.");
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            var selectedImage = button.DataContext as ImageItemDto;
            if (!ImageUploadViewModel.RemoveClick(selectedImage)) MessageBox.Show("Some error occured.");
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
