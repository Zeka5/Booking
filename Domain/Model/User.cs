using BookingApp.Serializer;
using System;

namespace BookingApp.Domain.Model
{
    public class User : ISerializable
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Vocation { get; set; }

        public User() { }

        public User(string username, string password, int vocation)
        {
            Username = username;
            Password = password;
            Vocation = vocation;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Username, Password, Vocation.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Username = values[1];
            Password = values[2];
            Vocation = Convert.ToInt32(values[3]);
        }
    }
}
