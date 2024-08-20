using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.Utilities;
using BookingApp.WPF.View.Guest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels
{
    public class RateOwnerPageViewModel
    {
        public AccommodationRatingService AccommodationRatingService { get; set; }
        public RenovateSuggestionService RenovateSuggestionService { get; set; }
        public int RenovationLevel { get; set; }
        public ImageService ImageService { get; set; }
        public ObservableCollection<string> Images { get; set; }
        public List<string> ImagesComboBox { get; set; }
        public AccommodationRatingDto AccommodationRatingDto { get; set; }
        public MyICommand ConfirmCommand { get; set; }
        public MyICommand<ComboBox> AddPictureCommand { get; set; }
        public MyICommand<string> RemovePictureCommand { get; set; }
        public NavigationService NavigationService { get; set; }
        private User user;
        private int accommodationReservationId;
        private int accommodationId;
        public string ImagePath { get; set; }
        public RateOwnerPageViewModel(User user,NavigationService navigationService,int accommodationReservationId, int accommodationId) {
            NavigationService = navigationService;
            this.user = user;
            this.accommodationReservationId = accommodationReservationId;
            this.accommodationId = accommodationId;
            ConfirmCommand = new MyICommand(ExecuteConfirm);
            AddPictureCommand = new MyICommand<ComboBox>(ExecuteAddingPicture);
            RemovePictureCommand = new MyICommand<string>(ExecuteRemovingPicture);
            RenovateSuggestionService = new RenovateSuggestionService(Injector.CreateInstance<IRenovateSuggestionRepository>());
            AccommodationRatingService = new AccommodationRatingService(Injector.CreateInstance<IAccommodationRatingRepository>());
            ImageService = new ImageService(Injector.CreateInstance<IImageRepository>());
            AccommodationRatingDto = new AccommodationRatingDto();
            Images = new ObservableCollection<string>();
            ImagesComboBox = new List<string>();
            UpdateImagesComboBox();
        }
        private void UpdateImagesComboBox()
        {
            foreach (var filePath in Directory.GetFiles("../../../Resources/Images/AccommodationImages", "*.*"))
            {
                if (filePath.EndsWith(".jpg") || filePath.EndsWith(".png"))
                {
                    string path = filePath.Substring(46);
                    ImagesComboBox.Add(path);
                }
            }
        }
        public void ExecuteConfirm() {
            AccommodationRatingDto.AccommodationReservationId = accommodationReservationId;
            AccommodationRatingService.Add(AccommodationRatingDto.ToAccommodationRating());
            if(RenovationLevel!=0) RenovateSuggestionService.Add(new RenovateSuggestion(accommodationReservationId,RenovationLevel));
            ImageService.UpdateMany(Images.ToList(), accommodationId);
            MessageBox.Show("Rating successfull!");
            NavigationService.Navigate(new MyReservations(user,0,NavigationService));
        }
        public void ExecuteAddingPicture(ComboBox comboBox) {
            ImagePath = "../../../Resources/Images/AccommodationImages/" + ImagePath;
            if(Images.Any(image => image==ImagePath)){
                MessageBox.Show("Image already added");
                return;
            }
            Images.Add(ImagePath);
            MessageBox.Show("Added successfully");
            comboBox.SelectedItem = null;
        }
        public void ExecuteRemovingPicture(string deleteImage) {
            string founded=string.Empty;
            foreach (string image in Images) {
                if (image == deleteImage)
                {
                    founded = image;
                }
            }
            Images.Remove(founded);
        }
    }
}
