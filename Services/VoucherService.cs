using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Repository;
using BookingApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class VoucherService
    {
        private IVoucherRepository voucherRepository;
        private TourReservationService tourReservationService;

        public VoucherService(IVoucherRepository voucherRepository,TourReservationService tourReservationService)
        {
            this.voucherRepository = voucherRepository;
            this.tourReservationService = tourReservationService;
        }

        public void Add(Voucher voucher)
        {
            voucherRepository.Add(voucher);
        }

        public void MakeVouchers(int tourRealizationId,int duration,string description,int guideId=-1)
        {
            foreach (var tourReservation in tourReservationService.GetAll())
            {
                if (tourReservation.TourRealizationId == tourRealizationId)
                {
                    Voucher voucher = new Voucher(tourReservation.UserId,duration,description,guideId);
                    Add(voucher);
                }
            }
        }
        public void LoadVouchers(ObservableCollection<VoucherDto> voucherList)
        {
            voucherList.Clear();

            foreach (var voucher in voucherRepository.GetAll())
            {
                if (voucher.Status == Domain.Model.ValidityStatus.VALID)
                {
                    voucherList.Add(new VoucherDto(voucher));
                }
            }
        }

        public List<Voucher> GetAllValidVouchers()
        {
            return voucherRepository.GetAll().FindAll(v => v.Status == Domain.Model.ValidityStatus.VALID);
        }

        public void Update(Voucher voucher)
        {
            voucherRepository.Update(voucher);
        }
    }
}
