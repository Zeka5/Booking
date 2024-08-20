using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.Utilities;
using BookingApp.WPF.View.Guest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels
{
    public class AccommodationViewViewModel : INotifyPropertyChanged
    {
        public AccommodationDto AccommodationDto { get; set; }
        public MyICommand NextPictureCommand { get; set; }
        public MyICommand PreviousPictureCommand { get; set; }
        public MyICommand NavigationToReservationCommand { get; set; }
        public NavigationService NavigationService { get; set; }
        private User user;
        public List<string> ImagePaths { get; set; }

        private string image;
        public string Image
        {
            get { return image; }

            set
            {
                if (value != image)
                {
                    image = value;
                    OnPropertyChanged(nameof(Image));
                }
            }
        }
        private int sideChanger;
        public ImageService ImageService { get; set; }
        public AccommodationViewViewModel(AccommodationDto accommodationDto, User user, NavigationService navigationService) {
            NavigationService = navigationService;
            this.user = user;
            NavigationToReservationCommand = new MyICommand(ExecuteNavigationToReservation);
            NextPictureCommand = new MyICommand(ExecuteNextPicture);
            PreviousPictureCommand = new MyICommand(ExecutePreviousCommand);
            ImageService = new ImageService(Injector.CreateInstance<IImageRepository>());
            AccommodationDto = accommodationDto;
            ImagePaths = new List<string>();
            GetImagePath();
            sideChanger = 0;
            if (ImagePaths.Count != 0) Image = ImagePaths[sideChanger];
        }
        private void GetImagePath()
        {
            ImageService.GetImagePath(AccommodationDto.Id,ImagePaths);
        }
        public void ExecuteNextPicture() {
            if (sideChanger == ImagePaths.Count - 1)
            {
                sideChanger = 0;
            }
            else
            {
                sideChanger++;
            }
            Image = ImagePaths[sideChanger];
        }
        public void ExecutePreviousCommand()
        {
            if (sideChanger == 0)
            {
                sideChanger = ImagePaths.Count - 1;
            }
            else
            {
                sideChanger--;
            }
            Image = ImagePaths[sideChanger];
        }
        private void ExecuteNavigationToReservation() {
            NavigationService.Navigate(new Reservation(AccommodationDto, user,NavigationService));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}