using BookingApp.Dto;
using BookingApp.WPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Commands
{
    public class AddImagesCommand : CommandBase
    {
        private ObservableCollection<ImageItemDto> addedImages;

        public AddImagesCommand(ObservableCollection<ImageItemDto> addedImages)
        {
            this.addedImages = addedImages;
        }

        public override void Execute(object? parameter)
        {
            var imageUpload = new ImageUpload(addedImages);
            imageUpload.ShowDialog();
        }
    }
}
