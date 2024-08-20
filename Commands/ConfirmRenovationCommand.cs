using BookingApp.Stores;
using BookingApp.WPF.ViewModels.OwnerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Commands
{
    public class ConfirmRenovationCommand : CommandBase
    {
        private readonly NavigationStore navigationStore;
        private readonly ScheduleRenovationViewModel scheduleRenovationViewModel;

        public ConfirmRenovationCommand(NavigationStore navigationStore, ScheduleRenovationViewModel scheduleRenovationViewModel)
        {
            this.navigationStore = navigationStore;
            this.scheduleRenovationViewModel = scheduleRenovationViewModel;
        }

        public override void Execute(object? parameter)
        {
            scheduleRenovationViewModel.RenovationViewModel.RangeStart = scheduleRenovationViewModel.SelectedDate;
            scheduleRenovationViewModel.RenovationViewModel.RangeEnd =
                scheduleRenovationViewModel.SelectedDate.AddDays(scheduleRenovationViewModel.RenovationViewModel.DaysToRenovate - 1);
            scheduleRenovationViewModel.AddRenovation(scheduleRenovationViewModel.RenovationViewModel.ToRenovation());
            navigationStore.CurrentViewModel = new HomeViewModel(navigationStore, scheduleRenovationViewModel.OwnerId);
        }
    }
}
