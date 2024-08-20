using BookingApp.WPF.ViewModels.OwnerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Commands
{
    public class CancelRenovationCommand : CommandBase
    {
        private readonly RenovationsDisplayViewModel renovationsDisplayViewModel;

        public CancelRenovationCommand(RenovationsDisplayViewModel renovationsDisplayViewModel)
        {
            this.renovationsDisplayViewModel = renovationsDisplayViewModel;
        }

        public override void Execute(object? parameter)
        {
            if (parameter is RenovationViewModel renovation)
            {
                if (DateTime.Today >= (renovation.RangeStart.AddDays(-5))) return;
                renovationsDisplayViewModel.DeleteRenovation(renovation.Id);
            }
        }
    }
}
