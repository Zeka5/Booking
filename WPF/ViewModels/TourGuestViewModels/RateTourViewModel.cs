using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.WPF.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.TourGuestViewModels
{
    public class RateTourViewModel : INotifyPropertyChanged
    {
        public ImageService imageService;
        public TourRatingService tourRatingService;
        public TourReservationService tourReservationService;
        public TourRealizationService tourRealizationService;
        public TourGuestService tourGuestService;
        public TourService tourService;
        public LocationService locationService;
        public LanguageService languageService;
        public ObservableCollection<string> Paths { get; set; }
        public List<TourGuest> TourGuests { get; set; }
        public TourReservation TourReservation { get; set; }
        public TourRatingDto TourRating { get; set; }
        public int SelectedTourRealizationId { get; set; }
        public Tour Tour {  get; set; }
        public int VisitorsCount { get; set; }

        public string currentTourGuest;

        public event PropertyChangedEventHandler? PropertyChanged;
        public string CurrentTourGuest
        {
            get { return currentTourGuest; }
            set
            {
                if (value != currentTourGuest)
                {
                    currentTourGuest = value;
                    OnPropertyChanged("CurrentTourGuest");
                }
            }
        }
        public string firstStar;
        public string FirstStar
        {
            get
            {
                return firstStar;
            }
            set
            {
                if (value != firstStar)
                {
                    firstStar = value;
                    OnPropertyChanged("FirstStar");
                }
            }
        }
        private string secondStar;
        public string SecondStar
        {
            get
            {
                return secondStar;
            }
            set
            {
                if (value != secondStar)
                {
                    secondStar = value;
                    OnPropertyChanged("SecondStar");
                }
            }
        }
        private string thirdStar;
        public string ThirdStar
        {
            get
            {
                return thirdStar;
            }
            set
            {
                if (value != thirdStar)
                {
                    thirdStar = value;
                    OnPropertyChanged("ThirdStar");
                }
            }
        }
        private string fourthStar;
        public string FourthStar
        {
            get
            {
                return fourthStar;
            }
            set
            {
                if (value != fourthStar)
                {
                    fourthStar = value;
                    OnPropertyChanged("FourthStar");
                }
            }
        }
        private string fifthStar;
        public string FifthStar
        {
            get
            {
                return fifthStar;
            }
            set
            {
                if (value != fifthStar)
                {
                    fifthStar = value;
                    OnPropertyChanged("FifthStar");
                }
            }
        }
        public RateTourViewModel(int selectedTourRealizationId)
        {
            TourRating = new TourRatingDto();

            tourService = new TourService(Injector.CreateInstance<ITourRepository>());
            locationService = new LocationService(Injector.CreateInstance<ILocationRepository>());
            languageService = new LanguageService(Injector.CreateInstance<ILanguageRepository>());
            tourRealizationService = new TourRealizationService(Injector.CreateInstance<ITourRealizationRepository>(), tourService, locationService, languageService, new TourReservationService(Injector.CreateInstance<ITourReservationRepository>()), new CheckPointService(Injector.CreateInstance<ICheckPointRepository>()));

            imageService = new ImageService(Injector.CreateInstance<IImageRepository>());
            tourRatingService = new TourRatingService(Injector.CreateInstance<ITourRatingRepository>(), new TourGuestService(Injector.CreateInstance<ITourGuestRepository>(), new TourReservationService(Injector.CreateInstance<ITourReservationRepository>())));
            tourReservationService = new TourReservationService(Injector.CreateInstance<ITourReservationRepository>());
            tourGuestService = new TourGuestService(Injector.CreateInstance<ITourGuestRepository>(),tourReservationService);

            Paths = new ObservableCollection<string>();
            SelectedTourRealizationId = selectedTourRealizationId;

            VisitorsCount = tourReservationService.GetByTourRelizationIdAndUserId(SelectedTourRealizationId, SignInForm.curretnUserId).VisitorsCount;

            TourReservation = tourReservationService.GetByTourRelizationIdAndUserId(SelectedTourRealizationId, SignInForm.curretnUserId);

            Tour = tourService.GetById(tourRealizationService.GetTourIdById(SelectedTourRealizationId));

            TourGuests = tourGuestService.GetByTourReservationId(TourReservation.Id);
            CurrentTourGuest = TourGuests[0].FullName;
            ResetStars();
        }
        public bool NextTourGuest()
        {
            AddTourRating();

            foreach (var path in Paths)
            {
                imageService.UpdateImage(path, tourRatingService.GetLast().Id, "TourRating");
            }

            if (VisitorsCount == 0)
            {
                TourReservation.IsRated = Rated.Rated;
                tourReservationService.Update(TourReservation);

                MessageBox.Show("You have entered ratings for all the tour guests!");
                return false;
            }
            Paths.Clear();
            ResetStars();
            return true;
        }
        private void AddTourRating()
        {
            TourRating tourRating = new TourRating(TourRating.Rating,
                                                    TourGuests[0].Id, TourRating.Comment);
            tourRatingService.Add(tourRating);

            TourGuests.RemoveAt(0);
            VisitorsCount--;

            UpdateCurrnetTourGuest();
        }
        private void UpdateCurrnetTourGuest()
        {
            if (TourGuests.Count != 0)
            {
                CurrentTourGuest = TourGuests[0].FullName;
            }
        }
        public bool AddPicture()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string filter = MakeFilter();
            if (filter == "") return false;
            openFileDialog.Filter = filter;
            openFileDialog.InitialDirectory = @"C:\Users\radmi\Desktop\SIMS Projekat\sims-ra-2024-group-5-team-a\Resources\Images";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)
            {
                string[] selectedImagePaths = openFileDialog.FileNames;
                if (selectedImagePaths.Length > 0)
                {
                    AddPaths(selectedImagePaths);
                    return true;
                }
            }
            return false;
        }
        private string MakeFilter()
        {
            string filter = imageService.GetAllUnassignedImages(this.Paths.ToList());
            if (filter == "") return "";
            return filter.Substring(0, filter.Length - 1);
        }
        private void AddPaths(string[] selectedPaths)
        {
            foreach (string selectedPath in selectedPaths)
            {

                string relativePath = "../../../";
                string[] absolutePath = selectedPath.Split("\\");
                relativePath += absolutePath[absolutePath.Length - 3] + "/" + absolutePath[absolutePath.Length - 2] + "/" + absolutePath[absolutePath.Length - 1];
                this.Paths.Add(relativePath);
            }
        }
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public void FirstStarClick()
        {
            FirstStar = @"\Resources\Images\star.png";
            SecondStar = @"\Resources\Images\starborder.png";
            ThirdStar = @"\Resources\Images\starborder.png";
            FourthStar = @"\Resources\Images\starborder.png";
            FifthStar = @"\Resources\Images\starborder.png";

            TourRating.Rating = 1;
        }
        public void SecondStarClick()
        {
            FirstStar = @"\Resources\Images\star.png";
            SecondStar = @"\Resources\Images\star.png";
            ThirdStar = @"\Resources\Images\starborder.png";
            FourthStar = @"\Resources\Images\starborder.png";
            FifthStar = @"\Resources\Images\starborder.png";

            TourRating.Rating = 2;
        }
        public void ThirdStarClick()
        {
            FirstStar = @"\Resources\Images\star.png";
            SecondStar = @"\Resources\Images\star.png";
            ThirdStar = @"\Resources\Images\star.png";
            FourthStar = @"\Resources\Images\starborder.png";
            FifthStar = @"\Resources\Images\starborder.png";

            TourRating.Rating = 3;
        }
        public void FourthStarClick()
        {
            FirstStar = @"\Resources\Images\star.png";
            SecondStar = @"\Resources\Images\star.png";
            ThirdStar = @"\Resources\Images\star.png";
            FourthStar = @"\Resources\Images\star.png";
            FifthStar = @"\Resources\Images\starborder.png";

            TourRating.Rating = 4;
        }
        public void FifthStarClick()
        {
            FirstStar = @"\Resources\Images\star.png";
            SecondStar = @"\Resources\Images\star.png";
            ThirdStar = @"\Resources\Images\star.png";
            FourthStar = @"\Resources\Images\star.png";
            FifthStar = @"\Resources\Images\star.png";

            TourRating.Rating = 5;
        }

        public void ResetStars()
        {
            FirstStar = @"\Resources\Images\starborder.png";
            SecondStar = @"\Resources\Images\starborder.png";
            ThirdStar = @"\Resources\Images\starborder.png";
            FourthStar = @"\Resources\Images\starborder.png";
            FifthStar = @"\Resources\Images\starborder.png";
        }
    }
}
