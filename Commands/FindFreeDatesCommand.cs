using BookingApp.WPF.ViewModels.OwnerViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.Commands
{
    public class FindFreeDatesCommand : CommandBase
    {
        private readonly ScheduleRenovationViewModel scheduleRenovationViewModel;

        public FindFreeDatesCommand(ScheduleRenovationViewModel scheduleRenovationViewModel)
        {
            this.scheduleRenovationViewModel = scheduleRenovationViewModel;

            this.scheduleRenovationViewModel.RenovationViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(scheduleRenovationViewModel.RenovationViewModel.DaysToRenovate)) OnCanExecuteChanged();
        }

        public override bool CanExecute(object? parameter)
        {
            return (scheduleRenovationViewModel.RenovationViewModel.DaysToRenovate > 0) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            scheduleRenovationViewModel.RangeStart = scheduleRenovationViewModel.RenovationViewModel.RangeStart;
            scheduleRenovationViewModel.RangeEnd = scheduleRenovationViewModel.RenovationViewModel.RangeEnd;

            var current = scheduleRenovationViewModel.RangeStart;
            while (current < scheduleRenovationViewModel.RangeEnd)
            {
                if (scheduleRenovationViewModel.IsDateFree(current, scheduleRenovationViewModel.RenovationViewModel.AccommodationId))
                {
                    current = current.AddDays(1);
                }
                else if ((current - scheduleRenovationViewModel.RangeStart).Days < scheduleRenovationViewModel.RenovationViewModel.DaysToRenovate)
                {
                    scheduleRenovationViewModel.RangeStart = current.AddDays(1);
                    current = scheduleRenovationViewModel.RangeStart;
                }
                else scheduleRenovationViewModel.RangeEnd = current;
            }

            if ((current - scheduleRenovationViewModel.RangeStart).Days >= scheduleRenovationViewModel.RenovationViewModel.DaysToRenovate)
            {
                scheduleRenovationViewModel.RangeEnd = current.AddDays(-scheduleRenovationViewModel.RenovationViewModel.DaysToRenovate);
                scheduleRenovationViewModel.DatePicker = Visibility.Visible;
            }
        }
    }
}
