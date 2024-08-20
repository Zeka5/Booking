using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using ResourceType = BookingApp.Domain.Model.ResourceType;

namespace BookingApp.Services
{
    public class ImageService
    {
        private IImageRepository imageRepository;

        public ImageService(IImageRepository imageRepository) { 
            this.imageRepository = imageRepository;
        }
      
        public ImageService()
        {
            imageRepository = Injector.CreateInstance<IImageRepository>();
        }

        public List<Image> GetAll()
        {
            return imageRepository.GetAll();
        }

        public Image? GetFirstByEntityAndType(int id, Domain.Model.ResourceType resourceType) {
            return imageRepository.GetFirstByEntityAndType(id, resourceType);
        }
        public void GetImagePath(int accommodationId, List<string> imagePaths)
        {
            List<Image>? images = imageRepository.GetByEntityAndType(accommodationId,ResourceType.Accommodation);
            foreach (Image image in images) { imagePaths.Add(image.Path); }
        }

        public void UpdateMany(List<string> images, int accommodationId)
        {
            imageRepository.UpdateMany(images, accommodationId);
        }

        public Image GetByPath(string imagePath)
        {
            return imageRepository.GetByPath(imagePath);
        }
      
        public bool Add(Image image)
        {
            if (imageRepository.ExistsWithPath(image.Path)) return false;
            imageRepository.Add(image);
            return true;
        }

        public void UpdateImage(string path,int entityId,string resourceType)
        {
            Image? image = imageRepository.GetByPath(path);
            image.EntityId = entityId;
            if (string.Equals(resourceType, "Accommodation")) image.ResourceType = ResourceType.Accommodation;
            else if (string.Equals(resourceType, "Tour")) image.ResourceType = ResourceType.Tour;
            else if (string.Equals(resourceType, "Driver")) image.ResourceType = ResourceType.Driver;
            else if (string.Equals(resourceType, "Vehicle")) image.ResourceType = ResourceType.Vehicle;
            else if (string.Equals(resourceType, "TourRating")) image.ResourceType = ResourceType.TourRating;
            imageRepository.Update(image);
        }

        public void AddImage(string path, int entityId, string resourceType)
        {
            Image image = new Image();
            image.Path = path;
            image.EntityId = entityId;
            if (string.Equals(resourceType, "Accommodation")) image.ResourceType = ResourceType.Accommodation;
            else if (string.Equals(resourceType, "Tour")) image.ResourceType = ResourceType.Tour;
            else if (string.Equals(resourceType, "Driver")) image.ResourceType = ResourceType.Driver;
            else if (string.Equals(resourceType, "Vehicle")) image.ResourceType = ResourceType.Vehicle;
            else if (string.Equals(resourceType, "TourRating")) image.ResourceType = ResourceType.TourRating;
            imageRepository.Add(image);
        }

        public string GetAllUnassignedImages(List<string> paths)
        {
            int pictureCounter = 0;
            string filter = "Slike|";
            foreach (Image image in imageRepository.GetUnassignedImages())
            {
                if (!paths.Contains(image.Path))
                {
                    filter += image.Path.Split('/')[5] + ";";
                    pictureCounter++;
                }
            }
            if (pictureCounter == 0)
            {
                return "";
            }
            return filter.Substring(0, filter.Length - 1);
        }

        public void Subscribe(IObserver observer)
        {
            imageRepository.Subscribe(observer);
        }
    }
}
