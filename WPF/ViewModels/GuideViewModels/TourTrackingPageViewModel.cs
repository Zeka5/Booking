using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.Utilities;
using BookingApp.WPF.View;
using BookingApp.WPF.View.Guide;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.GuideViewModels
{
    public class TourTrackingPageViewModel:INotifyPropertyChanged
    {
        public NavigationService NavService { get; set; }
        public MyICommand<CheckPointDto> CheckedCommand { get; set; }
        public MyICommand EndTourCommand { get; set; }
        public MyICommand TourGuestArrivedCommand {  get; set; }
        private int tourRealizationId;
        private string checkPointName;
        public bool TourFinished=false;
        public List<CheckPointDto> CheckPoints { get; set; }
        public ObservableCollection<TourGuestDto> TourGuests { get; set; }
        private TourGuestDto selectedTourGuest;
        public TourGuestDto SelectedTourGuest
        {
            get { return selectedTourGuest; }
            set
            {
                if (selectedTourGuest != value)
                {
                    selectedTourGuest = value;
                    OnPropertyChanged("TouristsArrived");
                    TourGuestArrivedCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public TourDto Tour { get; set; }

        private CheckPointService checkPointService;
        private TourRealizationService tourRealizationService;
        private TourGuestService tourGuestService;

        private bool touristsArrived=false;

        public bool TouristsArrived
        {
            get { return touristsArrived; }
            set
            {
                if (touristsArrived != value)
                {
                    touristsArrived = value;
                    OnPropertyChanged("TouristsArrived");
                }
            }
        }
        public TourTrackingPageViewModel(NavigationService navService,int tourRealizationId) 
        {
            NavService= navService;
            CheckedCommand = new MyICommand<CheckPointDto>(Execute_CheckedCommand);
            EndTourCommand = new MyICommand(Execute_EndTourCommand);
            TourGuestArrivedCommand = new MyICommand(Execute_TourGuestArrivedCommand, CanExecute_TourGuestArrivedCommand);
            checkPointService = new CheckPointService(Injector.CreateInstance<ICheckPointRepository>());
            tourGuestService = new TourGuestService(Injector.CreateInstance<ITourGuestRepository>(), new TourReservationService(Injector.CreateInstance<ITourReservationRepository>()));
            tourRealizationService = new TourRealizationService(Injector.CreateInstance<ITourRealizationRepository>(), new TourService(Injector.CreateInstance<ITourRepository>()), new LocationService(Injector.CreateInstance<ILocationRepository>()), new LanguageService(Injector.CreateInstance<ILanguageRepository>()), new TourReservationService(Injector.CreateInstance<ITourReservationRepository>()), new CheckPointService(Injector.CreateInstance<ICheckPointRepository>()));
            this.tourRealizationId = tourRealizationId;
            TourGuests = new ObservableCollection<TourGuestDto>();
            //SelectedTourGuest = new TourGuestDto();
            Tour = tourRealizationService.MakeTourDto(tourRealizationService.GetTourIdById(tourRealizationId));
            Tour.ExpectedTouristNumber = tourRealizationService.GetExpectedTouristNumber(tourRealizationId);
            CheckPoints = new List<CheckPointDto>();
            LoadCheckPoints(tourRealizationService.GetTourIdById(tourRealizationId), tourRealizationService.GetLastCheckPoint(tourRealizationId));
            if (tourRealizationService.FindStartedTour(SignInForm.curretnUserId) == null)
            {
                this.checkPointName = CheckPoints[0].Name;
                tourRealizationService.UpdateLastCheckPoint(this.tourRealizationId, 1);
            }
            else
            {
                this.checkPointName = CheckPoints[tourRealizationService.GetLastCheckPoint(tourRealizationId)-1].Name;
            }
            LoadTourGuests();
        }

        private void LoadCheckPoints(int tourId, int lastCheckPointIndex)
        {
            if (lastCheckPointIndex == -1)
            {
                 GetCheckPoints(tourId);
            }
            else
            {
                GetStartedTourCheckPoints(tourId, lastCheckPointIndex);
            }
        }

        private void GetCheckPoints(int tourId)
        {
            foreach (var checkPoint in checkPointService.GetAllByTourId(tourId))
            {
                    CheckPointDto checkPointDto = new CheckPointDto(checkPoint);
                    if (checkPointDto.Index == 1)
                    {
                        checkPointDto.IsChecked = true;
                        checkPointDto.IsEnabled = false;
                    }
                    CheckPoints.Add(checkPointDto);
            }
        }

        public void GetStartedTourCheckPoints(int tourId, int lastCheckPointIndex)
        {
            foreach (var checkPoint in checkPointService.GetAllByTourId(tourId))
            {
                    CheckPointDto checkPointDto = new CheckPointDto(checkPoint);
                    if (checkPointDto.Index <= lastCheckPointIndex)
                    {
                        checkPointDto.IsChecked = true;
                        checkPointDto.IsEnabled = false;
                    }
                    CheckPoints.Add(checkPointDto);
            }
        }

        private bool CanExecute_TourGuestArrivedCommand()
        {
            return SelectedTourGuest!= null;
        }

        private void Execute_TourGuestArrivedCommand()
        {
            tourGuestService.Update(SelectedTourGuest, this.checkPointName);
            TourGuests.Remove(SelectedTourGuest);
            if (TourGuests.Count == 0) TouristsArrived = true;
        }

        private void Execute_EndTourCommand()
        {
            FinishTour();
            MessageBox.Show("Tour has unexpectedly ended!");
            NavService.Navigate(new HomePage(NavService));
        }

        private void Execute_CheckedCommand(CheckPointDto checkPoint)
        {
                if (checkPoint.Index == CheckPoints.Count)
                {
                    UpdateEnabledStatus(checkPoint);
                    FinishTour();
                }
                else
                {
                if (CheckRemainingTourists(checkPoint)) return;
                    UpdateEnabledStatus(checkPoint);
                    this.checkPointName = checkPoint.Name;
                }
        }

        private void LoadTourGuests()
        {
            TourGuests.Clear();
            foreach (var tourGuest in tourGuestService.GetRemainingTourists(tourRealizationId))
            {
                TourGuests.Add(new TourGuestDto(tourGuest));
            }
        }

        public void ArrivedClick()
        {
            tourGuestService.Update(SelectedTourGuest, this.checkPointName);
            TourGuests.Remove(SelectedTourGuest);
            if(TourGuests.Count==0) TouristsArrived = true;
        }

        private void UpdateEnabledStatus(CheckPointDto checkPoint)
        {
            checkPoint.IsEnabled = false;
            tourRealizationService.UpdateLastCheckPoint(this.tourRealizationId, checkPoint.Index);
        }

        private bool CheckRemainingTourists(CheckPointDto checkPoint)
        {
            if (tourGuestService.GetRemainingTourists(this.tourRealizationId).Count == 0)
            {
                checkPoint.IsEnabled = false;
                return true;
            }
            return false;
        }

        private void FinishTour()
        {
            tourRealizationService.UpdateTourRealizationState(this.tourRealizationId, "Finished");
            MessageBox.Show("Tour is finished!");
            NavService.Navigate(new HomePage(NavService));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
