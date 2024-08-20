using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Repository;
using BookingApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class TourService
    {
        private ITourRepository tourRepository;

        public TourService(ITourRepository tourRepository)
        {
            this.tourRepository = tourRepository;
        }

        public Tour Add(Tour tour)
        {
            return tourRepository.Add(tour);
        }
        public Tour GetById(int id)
        {
            return tourRepository.GetById(id);
        }
        public int GetLastId()
        {
            return tourRepository.GetLast().Id;
        }

        public int GetUserId(int tourId)
        {
            Tour tour=tourRepository.GetById(tourId);
            return tour.UserId;
        }

        public void UpdateTourPriority(int userId,bool isFromSuperGuide)
        {
            foreach(var tour in tourRepository.GetAll())
            {
                if(tour.UserId == userId)
                {
                    tour.IsFromSuperGuide=isFromSuperGuide;
                    tourRepository.Update(tour);
                }
            }
        }
    }
}
