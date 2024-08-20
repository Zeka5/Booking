using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class GuideRepository:IGuideRepository
    {
        private const string FilePath = "../../../Resources/Data/guides.csv";

        private readonly Serializer<Guide> serializer;

        private List<Guide> guides;

        public GuideRepository()
        {
            serializer = new Serializer<Guide>();
            guides = serializer.FromCSV(FilePath);
        }

        public Guide GetByUserId(int id)
        {
            return guides.FirstOrDefault(g => g.UserId == id);
        }

        public Guide Update(Guide guide)
        {
            guides = serializer.FromCSV(FilePath);
            Guide current = guides.Find(g => g.Id == guide.Id);
            int index = guides.IndexOf(current);
            guides.Remove(current);
            guides.Insert(index, guide);
            serializer.ToCSV(FilePath, guides);
            return guide;
        }
    }
}
