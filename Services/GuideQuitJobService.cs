using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class GuideQuitJobService
    {
        private TourRealizationService tourRealizationService;
        private VoucherService voucherService;
        public GuideQuitJobService(TourRealizationService tourRealizationService, VoucherService voucherService)
        {
            this.tourRealizationService = tourRealizationService;
            this.voucherService = voucherService;
        }

        public void Quit(int userId)
        {
            foreach(var tourRealization in tourRealizationService.GetAllByUser(userId))
            {
                if (tourRealization.StartTime.Date >= DateTime.Now.Date && tourRealization.TourState.ToString().Equals("None"))
                {
                    tourRealizationService.UpdateTourRealizationState(tourRealization.Id, "Cancelled");
                    voucherService.MakeVouchers(tourRealization.Id,24, "Received after a guide resigned from his job");
                }
            }
            UpdateVouchers(userId);
        }

        private void UpdateVouchers(int userId)
        {
            foreach(var voucher in voucherService.GetAllValidVouchers())
            {
                if(voucher.GuideId== userId)
                {
                    voucher.GuideId = -1;
                    voucherService.Update(voucher);
                }
            }
        }
    }
}
