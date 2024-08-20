using BookingApp.Stores;
using BookingApp.WPF.ViewModels.OwnerViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Commands
{
    public class RegisterAccommodationCommand : CommandBase
    {
        private readonly NavigationStore navigationStore;
        private readonly RegisterAccommodationViewModel viewModel;

        public RegisterAccommodationCommand(NavigationStore navigationStore, RegisterAccommodationViewModel viewModel)
        {
            this.navigationStore = navigationStore;
            this.viewModel = viewModel;

            viewModel.AccommodationDto.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            bool propertiesChanged =
                e.PropertyName == nameof(viewModel.AccommodationDto.Name) ||
                e.PropertyName == nameof(viewModel.AccommodationDto.LocationDto) ||
                e.PropertyName == nameof(viewModel.AccommodationDto.Type) ||
                e.PropertyName == nameof(viewModel.AccommodationDto.MaxGuest) ||
                e.PropertyName == nameof(viewModel.AccommodationDto.MinStayDays) ||
                e.PropertyName == nameof(viewModel.AccommodationDto.CancellationDays);
            if (propertiesChanged) OnCanExecuteChanged();
        }

        public override bool CanExecute(object? parameter)
        {
            return viewModel.ValidateValues && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            viewModel.Submit();
            navigationStore.CurrentViewModel = new HomeViewModel(navigationStore, viewModel.AccommodationDto.OwnerId);
        }
    }
}
