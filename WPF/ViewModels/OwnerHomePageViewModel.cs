using BookingApp.Commands;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Services;
using BookingApp.WPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels
{
    public class OwnerHomePageViewModel : Observer.IObserver, INotifyPropertyChanged
    {
        private User user;

        private int userId;
        public int UserId
        {
            get { return userId; }
            set
            {
                if (userId != value)
                {
                    userId = value;
                    OnPropertyChanged();
                }
            }
        }
        private OwnerHomePageService ownerHomePageService;
        public ObservableCollection<AccommodationDto> Accommodations { get; }

        public ICommand StatsCommand { get; }

        public OwnerHomePageViewModel(User user)
        {
            this.user = user;
            UserId = this.user.Id;
            this.ownerHomePageService = new OwnerHomePageService(user.Id);
            Accommodations = new ObservableCollection<AccommodationDto>();

            //StatsCommand = new OpenStatsCommand(mainFrame);

            ownerHomePageService.SubscribeAccommodations(this);
            Update();
        }

        public void RegisterAccommodation()
        {
            var registerAccommodation = new RegisterAccommodation(user);
            registerAccommodation.ShowDialog();
        }

        public void Update()
        {
            ownerHomePageService.Update(Accommodations);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
