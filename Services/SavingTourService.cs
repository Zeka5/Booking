using BookingApp.Domain.Model;
using BookingApp.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class SavingTourService
    {
        private TourService tourService;
        private TourRealizationService tourRealizationService;
        private ImageService imageService;
        private CheckPointService checkPointService;

        public SavingTourService(TourService tourService, TourRealizationService tourRealizationService, ImageService imageService, CheckPointService checkPointService)
        {
            this.tourService = tourService;
            this.tourRealizationService = tourRealizationService;
            this.imageService = imageService;
            this.checkPointService = checkPointService;
        }

        public void SaveTour(Tour tour, List<DateTime> tourStarts, List<CheckPoint> checkPoints, List<string> imagePaths)
        {
            tourService.Add(tour);
            int tourId = tourService.GetLastId();
            SaveImages(imagePaths,tourId);
            SaveCheckPoints(checkPoints,tourId);
            SaveTourRealizations(tourStarts,tour.Capacity);
        }

        private void SaveTourRealizations(List<DateTime> tourStarts, int capacity)
        {
            foreach (var date in tourStarts)
            {
                tourRealizationService.Save(date,capacity);
            }
        }

        private void SaveCheckPoints(List<CheckPoint> checkPoints, int tourId)
        {
            foreach (var checkPoint in checkPoints)
            {
                checkPointService.Save(checkPoint, tourId);
            }
        }

        private void SaveImages(List<string> imagePaths, int tourId)
        {
            foreach (var path in imagePaths)
            {
                imageService.AddImage(path, tourId, "Tour");
            }
        }

        public int GetLastTourRealizationId()
        {
            return tourRealizationService.GetLast().Id;
        }
    }
}
