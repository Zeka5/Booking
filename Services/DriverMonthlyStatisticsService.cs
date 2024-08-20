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
    public class DriverMonthlyStatisticsService
    {
        private IDriveRepository driveRepository;

        public DriverMonthlyStatisticsService()
        {
            driveRepository = Injector.CreateInstance<IDriveRepository>();
        }

        public List<Drive> GetByMonthAndYear(int driverId, int month, int year)
        {
            return driveRepository.GetByMonthAndYear(driverId, month, year);
        }
    }
}
