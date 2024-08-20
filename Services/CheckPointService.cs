using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Repository;
using BookingApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class CheckPointService
    {
        private ICheckPointRepository checkPointRepository;

        public CheckPointService(ICheckPointRepository checkPointRepository)
        {
            this.checkPointRepository = checkPointRepository;
        }
      
        public void Save(CheckPoint checkPoint, int tourId)
        {
            checkPoint.TourId = tourId;
            checkPointRepository.Add(checkPoint);
        }

        public List<CheckPoint> GetAllByTourId(int tourId)
        {
            List<CheckPoint> checkPoints=new List<CheckPoint>();
            foreach(var checkPoint in checkPointRepository.GetAll()) 
            {
                if (checkPoint.TourId == tourId)
                {
                    checkPoints.Add(checkPoint);
                }
            }
            return checkPoints;
        }
        
        public CheckPoint GetById(int id)
        {
            return checkPointRepository.GetAll().Find(ch => ch.Id == id);
        }
    }
}
