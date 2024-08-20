﻿using BookingApp.Domain.Model;
using BookingApp.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IVehicleRepository
    {
        List<Vehicle> GetAll();
        Vehicle GetById(int id);
        Vehicle GetByDriverId(int driverId);
        int GetNextId();
        Vehicle Add(Vehicle vehicle);
        void Delete(Vehicle vehicle);
        void Subscribe(IObserver observer);
    }
}
