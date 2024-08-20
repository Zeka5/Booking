using BookingApp.Dto;
using BookingApp.WPF.ViewModels.OwnerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Commands
{
    public class AcceptModificationRequestCommand : CommandBase
    {
        private readonly ModifyReservationsViewModel viewModel;

        public AcceptModificationRequestCommand(ModifyReservationsViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            if (parameter is ReservationChangeRequestDto request)
            {
                viewModel.Accept(request.ToReservationChangeRequest());
            }
        }
    }
}
