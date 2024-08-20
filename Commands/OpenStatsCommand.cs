using BookingApp.Stores;
using BookingApp.WPF.ViewModels.OwnerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Commands
{
    public class OpenStatsCommand : CommandBase
    {
        private readonly NavigationStore navigationStore;

        public OpenStatsCommand(NavigationStore navigationStore)
        {
            this.navigationStore = navigationStore;
        }

        public override void Execute(object? parameter)
        {
            if (parameter is AccommodationViewModel accommodation)
            {
                navigationStore.CurrentViewModel = new OverallStatsViewModel(navigationStore, accommodation);
            }
        }
    }
}
