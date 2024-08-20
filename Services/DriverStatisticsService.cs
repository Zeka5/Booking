using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class DriverStatisticsService
    {
        private IDriveRepository driveRepository;

        public DriverStatisticsService()
        {
            driveRepository = Injector.CreateInstance<IDriveRepository>();
        }

        public List<Drive> GetByDriverAndYear(int driverId, int month)
        {
            return driveRepository.GetByDriverAndYear(driverId, month);
        }

        public int GetMinYear()
        {
            return driveRepository.GetAll().Min(d => d.EndTime.Year);
        }
    }
}
