using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public enum Mode { Guest , SuperGuest}
    public class Guest : ISerializable
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public Mode Mode { get; set; }
        public int BonusPoints { get; set; }
        public DateTime SuperGuestConfigured {  get; set; }
        public Guest() { }
        public Guest(int id, string username) { 
            Id = id;
            Username = username;
            Mode = Mode.Guest;
            BonusPoints = 0;
        }
        public Guest(string username, Mode mode, int bonusPoints, DateTime dateTime) 
        {
            Username = username;
            Mode = mode;
            BonusPoints = bonusPoints;
            SuperGuestConfigured = dateTime;
        }

        public string[] ToCSV()
        {
            string[] values =
            {
                Id.ToString(),
                Username,
                Mode.ToString(),
                BonusPoints.ToString(),
                SuperGuestConfigured.ToString("dd/MM/yyyy"),
            };
            return values;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Username = values[1];
            Mode = Enum.TryParse(values[2] ,out Mode mode) ? mode : Mode.Guest;
            BonusPoints = int.Parse(values[3]);
            string[] timeValues = values[4].Split("/");
            SuperGuestConfigured = new DateTime(int.Parse(timeValues[2]), int.Parse(timeValues[1]), int.Parse(timeValues[0]));
        }
    }
}
