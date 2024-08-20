using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.WPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.ViewModels.TourGuestViewModels
{
    public class VoucherUsageViewModel
    {
        public VoucherService voucherService;
        public static ObservableCollection<VoucherDto> VoucherList { get; set; }
        public VoucherDto? SelectedVoucher { get; set; }
        public int TourRealizationId { get; set; }
        public BookingApp.WPF.View.TourRealizationPage PreviousPage { get; set; }
        public VoucherUsageViewModel(int tourRealizationId, BookingApp.WPF.View.TourRealizationPage previousPage)
        {
            voucherService = new VoucherService(Injector.CreateInstance<IVoucherRepository>(),new TourReservationService(Injector.CreateInstance<ITourReservationRepository>()));
            VoucherList = new ObservableCollection<VoucherDto>();
            TourRealizationId = tourRealizationId;
            PreviousPage = previousPage;

            LoadVouchers();
        }
        private void LoadVouchers()
        {
            voucherService.LoadVouchers(VoucherList);
        }
        public void UseVaucher()
        {
            if (SelectedVoucher == null)
            {
                MessageBox.Show("Select a voucher!");
                return;
            }

            ReserveTour reserveTour = new ReserveTour(TourRealizationId, SelectedVoucher.Id);
            reserveTour.Show();
        }
    }
}
