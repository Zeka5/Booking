using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Observer;
using BookingApp.Serializer;

namespace BookingApp.Repository
{
    public class ImageRepository : IImageRepository
    {
        private const string filePath = "../../../Resources/Data/images.csv";
        private readonly Serializer<Image> serializer;
        private List<Image> images;
        public Subject ImageSubject;

        public ImageRepository()
        {
            this.serializer = new Serializer<Image>();
            this.images = this.serializer.FromCSV(filePath);
            ImageSubject = new Subject();
        }

        public Image? GetById(int id)
        {
            return this.images.Find(x => x.Id == id);
        }

        public void UpdateMany(List<string> imagePaths , int entityId) {
            foreach (string imagePath in imagePaths) {
                Image? image = new Image(imagePath,entityId,ResourceType.Accommodation);
                Add(image);
            }
        }
        public void Update(Image image)
        {
            Image? oldImage = GetById(image.Id);
            if (oldImage == null) { return; }

            oldImage.Path = image.Path;
            oldImage.EntityId = image.EntityId;
            oldImage.ResourceType = image.ResourceType;

            serializer.ToCSV(filePath, images);
        }
        public int GetNextId()
        {
            if (images.Count < 1)
            {
                return 1;
            }

            return images.Max(v => v.Id) + 1;
        }

        public void Add(Image image)
        {
            images = serializer.FromCSV(filePath);
            image.Id = GetNextId();
            /*if (images.Any(v => v.Path==image.Path))
            {
                return;
            }*/
            images.Add(image);
            serializer.ToCSV(filePath, images);
            ImageSubject.NotifyObservers();
        }

        public List<Image> GetAll()
        {
            return this.images;
        }
        public List<Image> GetUnassignedImages()
        {
            return this.images.FindAll(x => x.EntityId == -1 && x.ResourceType == ResourceType.None);
        }

        public Image? GetByPath(string path)
        {
            return this.images.Find(x => x.Path == path);
        }

        public Image? GetByEntityId(int entityId)
        {
            return this.images.Find(x => x.EntityId == entityId);
        }

        public List<Image>? GetByEntityAndType(int entityId, ResourceType resourceType)
        {
            return this.images.FindAll(x => x.EntityId == entityId && x.ResourceType == resourceType);
        }
        public Image? GetFirstByEntityAndType(int entityId, ResourceType resourceType)
        {
            return this.images.Find(x => x.EntityId == entityId && x.ResourceType == resourceType);
        }
        public void Delete(Image image)
        {
            images = serializer.FromCSV(filePath);
            Image foundImage = images.Find(i => i.Id == image.Id);

            if(foundImage == null) { return; }
            images.Remove(foundImage);

            serializer.ToCSV(filePath, images);
            ImageSubject.NotifyObservers();
        }

        public void DeleteByVehicleId(int vehicleId)
        {
            List<Image>? images = GetByEntityAndType(vehicleId, ResourceType.Vehicle);
            foreach (Image image in images)
            {
                Delete(image);
            }
        }

        public bool ExistsWithPath(string path)
        {
            return images.Any(image => image.Path == path);
        }

        public void Subscribe(IObserver observer)
        {
            ImageSubject.Subscribe(observer);
        }

    }
}
