using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class ImageUploadService
    {
        private const string corePath = "../../../Resources/Images/AccommodationImages/";
        private IImageRepository imageRepository;
        public ImageUploadService()
        {
            imageRepository = Injector.CreateInstance<IImageRepository>();
        }

        private ImageItemDto GetImageItem(string imagePath)
        {
            string name = Path.GetFileName(imagePath);
            string path = imagePath.Substring(8); // odsece se '../../..' zato da bi se u xaml radio bind
            return new ImageItemDto(name, path);
        }

        public void LoadImages(List<ImageItemDto> images)
        {
            images.Clear();
            if (Directory.Exists(corePath))
            {
                string[] imageFiles = Directory.GetFiles(corePath, "*.jpg", SearchOption.TopDirectoryOnly);
                foreach (string imagePath in imageFiles)
                    if (!imageRepository.ExistsWithPath(imagePath))
                        images.Add(GetImageItem(imagePath));
            }
            else Console.WriteLine("Folder does not exists.");
        }
    }
}
