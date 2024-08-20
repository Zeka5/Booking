using BookingApp.Domain.Model;
using BookingApp.Dto;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.View
{
    /// <summary>
    /// Interaction logic for TourGuestNotifications.xaml
    /// </summary>
    public partial class TourGuestNotifications : Page
    {
        public ObservableCollection<TourGuestDto> TourGuestNotificationsList { get; set; }
        public ObservableCollection<TourRequestNotificationDto> TourRequestNotificationList { get; set; }
        public ObservableCollection<GroupDriveReservationDto> GroupDriveReservationList { get; set; }
        public List<BookingApp.Domain.Model.TourGuest> tourGuests { get; set; }
        public TourGuestRepository TourGuestRepository { get; set; }
        public TourRequestNotificationRepository TourRequestNotificationRepository { get; set; }
        public TourRequestRepository TourRequestRepository { get; set; }
        public GroupDriveReservationRepository GroupDriveReservationRepository { get; set; }
        public LocationRepository LocationRepository { get; set; }
        public AddressRepository AddressRepository { get; set; }
        public TourGuestNotifications()
        {
            InitializeComponent();
            DataContext = this;

            TourGuestNotificationsList = new ObservableCollection<TourGuestDto>();
            TourRequestNotificationList = new ObservableCollection<TourRequestNotificationDto>();
            GroupDriveReservationList = new ObservableCollection<GroupDriveReservationDto>();
            TourGuestRepository = new TourGuestRepository();
            TourRequestNotificationRepository = new TourRequestNotificationRepository();
            LocationRepository = new LocationRepository();
            AddressRepository = new AddressRepository();
            TourRequestRepository = new TourRequestRepository();
            GroupDriveReservationRepository = new GroupDriveReservationRepository();
            tourGuests = new List<BookingApp.Domain.Model.TourGuest>();

            LoadTourGuestArrivalsNotificiations();
            LoadTourRequestNotifications();
            LoadGroupReservationNotifications();
        }
        private void LoadGroupReservationNotifications()
        {
            GroupDriveReservationList.Clear();

            foreach(var groupDriveReservation in GroupDriveReservationRepository.GetAll().Where(gdr => gdr.Status == GroupDriveStatus.Processed && gdr.IsRead == false))
            {
                string startAddress = AddressRepository.GetById(groupDriveReservation.StartAddressId).Street;
                string endAddress = AddressRepository.GetById(groupDriveReservation.EndAddressId).Street;
                GroupDriveReservationList.Add(new GroupDriveReservationDto(groupDriveReservation, startAddress, endAddress));
            }
        }
        private void LoadTourGuestArrivalsNotificiations()
        {
            TourGuestNotificationsList.Clear();

            foreach(BookingApp.Domain.Model.TourGuest tourGuest in TourGuestRepository.GetAll().Where(tg => tg.ArrivalNotification == false && tg.CheckPointName != "Not arrived"))
            {
                tourGuests.Add(tourGuest);
                TourGuestNotificationsList.Add(new TourGuestDto(tourGuest));
            }
        }
        private void LoadTourRequestNotifications()
        {
            TourRequestNotificationList.Clear();

            foreach (var notification in TourRequestNotificationRepository.GetAll())
            {
                if(!notification.IsRead)
                {
                    var location = LocationRepository.GetById(TourRequestRepository.GetById(notification.TourRequestId).LocationId);
                    TourRequestNotificationList.Add(new TourRequestNotificationDto(notification, location));
                }
            }
        }
        private void DismissTourGuestArrivalsClick(object sender, RoutedEventArgs e)
        {
            foreach(var tourGuest in tourGuests)
            {
                tourGuest.ArrivalNotification = true;
                TourGuestRepository.Update(tourGuest);
            }

            TourGuestNotificationsList.Clear();
        }
        private void DismissTourRequestsClick(object sender, RoutedEventArgs e)
        {
            foreach (var tourRequestNotification in TourRequestNotificationList)
            {
                tourRequestNotification.IsRead = true;
                TourRequestNotificationRepository.Update(tourRequestNotification.ToTourRequestNotification());
            }

            TourRequestNotificationList.Clear();
        }
        private void DismissGroupDriveReservationsClick(object sender, RoutedEventArgs e)
        {
            foreach (var groupDriveReservation in GroupDriveReservationList)
            {
                /*groupDriveReservation.IsRead = true;
                TourRequestNotificationRepository.Update(groupDriveReservation.ToGroupDriveReservation());*/
            }

            TourRequestNotificationList.Clear();
        }
    }
}
