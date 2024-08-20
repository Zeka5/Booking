using BookingApp.Domain.Model;
using BookingApp.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IImageRepository
    {
        Image? GetById(int id);
        void UpdateMany(List<string> imagePaths, int entityId);
        void Update(Image image);
        void Add(Image image);
        List<Image> GetAll();
        List<Image> GetUnassignedImages();
        Image? GetByPath(string path);
        Image? GetByEntityId(int entityId);
        List<Image>? GetByEntityAndType(int entityId, ResourceType resourceType);
        Image? GetFirstByEntityAndType(int entityId, ResourceType resourceType);
        void Delete(Image image);
        void DeleteByVehicleId(int vehicleId);
        bool ExistsWithPath(string path);
        void Subscribe(IObserver observer);
    }
}
