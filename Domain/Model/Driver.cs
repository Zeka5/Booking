using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using BookingApp.Serializer;

namespace BookingApp.Domain.Model
{
    public class Driver : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public bool IsSuperDriver { get; set; }
        public int Points { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime MembershipDate { get; set; }
        public int UnreliableCount { get; set; }
        public bool IsFirstLogIn { get; set; }

        public Driver() { }

        public Driver(string name, string lastName, int age, string email, string phone, DateTime membershipDate, int unreliableCount=0)
        {
            Name = name;
            LastName = lastName;
            IsSuperDriver = false;
            Points = 0;
            Age = age;
            Email = email;
            Phone = phone;
            MembershipDate = membershipDate;
            UnreliableCount = unreliableCount;
            IsFirstLogIn = true;
        }

        public Driver(string name, string lastName, int age, string email, string phone)
        {
            Name = name;
            LastName = lastName;
            IsSuperDriver = false;
            Points = 0;
            Age = age;
            Email = email;
            Phone = phone;
            MembershipDate = DateTime.Now;
            IsFirstLogIn=true;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            LastName = values[2];
            IsSuperDriver = Convert.ToBoolean(values[3]);
            Points = Convert.ToInt32(values[4]);
            Age = Convert.ToInt32(values[5]);
            Email = values[6];
            Phone = values[7];
            MembershipDate = DateTime.ParseExact(values[8], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            UnreliableCount = Convert.ToInt32(values[9]);
            IsFirstLogIn = Convert.ToBoolean(values[10]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Name,
                LastName,
                IsSuperDriver.ToString(),
                Points.ToString(),
                Age.ToString(),
                Email,
                Phone,
                MembershipDate.ToString("dd/MM/yyyy",CultureInfo.InvariantCulture),
                UnreliableCount.ToString(),
                IsFirstLogIn.ToString()
            };
            return csvValues;
        }
    }
}
