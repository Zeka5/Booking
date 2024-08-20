using BookingApp.Serializer;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace BookingApp.Domain.Model
{
    public class Tour : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
        public string Description { get; set; }
        public int LanguageId { get; set; }
        public int Capacity { get; set; }
        public double Duration { get; set; }
        public int UserId {  get; set; }
        public bool IsFromSuperGuide {  get; set; }

        public Tour()
        {
        }

        public Tour(int id, string name, int locationId, string description, int languageId,

                    int capacity, double duration,int userId,bool isFromSuperGuide=false)
        {
            Id = id;
            Name = name;
            LocationId = locationId;
            Description = description;
            LanguageId = languageId;
            Capacity = capacity;
            Duration = duration;
            UserId= userId;
            IsFromSuperGuide = isFromSuperGuide;

        }


        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            LocationId = Convert.ToInt32(values[2]);
            Description = values[3];
            LanguageId = Convert.ToInt32(values[4]);
            Capacity = Convert.ToInt32(values[5]);
            Duration = Convert.ToDouble(values[6]);
            UserId = Convert.ToInt32(values[7]);
            IsFromSuperGuide = Convert.ToBoolean(values[8]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                Name,
                LocationId.ToString(),
                Description,
                LanguageId.ToString(),
                Capacity.ToString(),
                Duration.ToString(),
                UserId.ToString(),
                IsFromSuperGuide.ToString()
            };
            return csvValues;
        }
    }
}
