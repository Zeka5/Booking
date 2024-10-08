﻿using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class TourRequestTourGuest : ISerializable
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public int TourRequestId { get; set; }
        public TourRequestTourGuest()
        {

        }
        public TourRequestTourGuest(string fullName, int age)
        {
            FullName = fullName;
            Age = age;
        }
        public TourRequestTourGuest(int id, string fullName, int age, int tourRequestId)
        {
            Id = id;
            FullName = fullName;
            Age = age;
            TourRequestId = tourRequestId;
        }
        public TourRequestTourGuest(string fullName, int age, int tourRequestId)
        {
            FullName = fullName;
            Age = age;
            TourRequestId = tourRequestId;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            FullName = values[1];
            Age = Convert.ToInt32(values[2]);
            TourRequestId = Convert.ToInt32(values[3]);
        }
        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(),
                                   FullName,
                                   Age.ToString(),
                                   TourRequestId.ToString() };
            return csvValues;
        }
    }
}
