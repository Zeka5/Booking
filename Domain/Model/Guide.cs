using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class Guide:ISerializable
    {   
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Biography {  get; set; }
        public bool IsSuperGuide { get; set; }
        public DateOnly SuperGuideStartDate {  get; set; }
        public Guide() { }

        public Guide(int id,int userId,string firstName,string lastName,string email,string phoneNumber,string biography,bool isSuperGuide,DateOnly superGuideStartDate)
        {
            Id=id;
            UserId=userId;
            FirstName=firstName;
            LastName=lastName;
            Email=email;
            PhoneNumber=phoneNumber;
            Biography=biography;
            IsSuperGuide=isSuperGuide;
            SuperGuideStartDate=superGuideStartDate;
        }

        public new string[] ToCSV()
        {
            string[] csvValues = 
                { 
                    Id.ToString(),UserId.ToString(),FirstName, LastName, Email, PhoneNumber,Biography,IsSuperGuide.ToString(),SuperGuideStartDate.ToString("dd/MM/yyyy")
                };
            return csvValues;
        }

        public new void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            UserId = Convert.ToInt32(values[1]);
            FirstName = values[2];
            LastName = values[3];
            Email = values[4];
            PhoneNumber = values[5];
            Biography = values[6];
            IsSuperGuide = Convert.ToBoolean(values[7]);
            SuperGuideStartDate = DateOnly.ParseExact(values[8], "dd/MM/yyyy");
        }

    }
}
