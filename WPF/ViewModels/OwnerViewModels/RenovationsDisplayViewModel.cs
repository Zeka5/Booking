using BookingApp.Commands;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Observer;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.OwnerViewModels
{
    public class RenovationsDisplayViewModel : ViewModelBase, IObserver
    {
        private readonly RenovationService renovationService;
        private readonly int ownerId;

        public ObservableCollection<RenovationViewModel> Renovations { get; }

        public ICommand CancelRenovation { get; }

        public RenovationsDisplayViewModel(int ownerId)
        {
            renovationService = new RenovationService(Injector.CreateInstance<IRenovationRepository>(),
                new AccommodationService(Injector.CreateInstance<IAccommodationRepository>(),
                    new ImageService(Injector.CreateInstance<IImageRepository>())));
            this.ownerId = ownerId;

            renovationService.SubscribeRenovations(this);
            Renovations = new ObservableCollection<RenovationViewModel>();

            CancelRenovation = new CancelRenovationCommand(this);

            Update();
        }

        public void Update()
        {
            Renovations.Clear();
            foreach (var renovation in renovationService.GetRenovationByOwnerId(ownerId))
            {
                var imagePath = renovationService.GetImageByEntityId(renovation.AccommodationId).Path ??
                    Image.defaultAccommodationImagePath;
                var accommodationName = renovationService.GetAccommodationById(renovation.AccommodationId).Name;
                Renovations.Add(new RenovationViewModel(renovation.Id, renovation.AccommodationId, imagePath, accommodationName,
                    renovation.StartDate, renovation.EndDate));
            }
        }

        public void DeleteRenovation(int id)
        {
            renovationService.DeleteRenovation(id);
        }
    }
}
