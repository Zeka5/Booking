using BookingApp.Dto;
using BookingApp.Services;
using BookingApp.WPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels
{
    public class ImageUploadViewModel
    {
        private ImageUploadService imageUploadService;
        public List<ImageItemDto> Images { get; }
        public ObservableCollection<ImageItemDto> AddedImages { get; set; }

        public ImageUploadViewModel(ObservableCollection<ImageItemDto> addedImages)
        {
            imageUploadService = new ImageUploadService();
            Images = new List<ImageItemDto>();
            AddedImages = addedImages;

            imageUploadService.LoadImages(Images);
        }

        public bool AddClick(ImageItemDto image)
        {
            if (AddedImages.Any(i => i.Equals(image))) return false;
            AddedImages.Add(image);
            return true;
        }

        public bool RemoveClick(ImageItemDto image)
        {
            if (!AddedImages.Any(i => i.Equals(image))) return false;
            AddedImages.Remove(image);
            return true;
        }
    }
}
