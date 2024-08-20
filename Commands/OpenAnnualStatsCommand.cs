using BookingApp.Stores;
using BookingApp.WPF.ViewModels.OwnerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Commands
{
    public class OpenAnnualStatsCommand : CommandBase
    {
        private readonly NavigationStore navigationStore;
        private readonly AccommodationViewModel accommodation;

        public OpenAnnualStatsCommand(NavigationStore navigationStore, AccommodationViewModel accommodation)
        {
            this.navigationStore = navigationStore;
            this.accommodation = accommodation;
        }

        public override void Execute(object? parameter)
        {
            if (parameter is AnnualTotalsViewModel annualTotals)
            {
                navigationStore.CurrentViewModel = new AnnualStatsViewModel(navigationStore, annualTotals.Year, accommodation);
            }
        }
    }
}
