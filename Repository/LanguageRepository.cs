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
    public class LanguageRepository:ILanguageRepository
    {
        private const string filePath = "../../../Resources/Data/languages.csv";
        private readonly Serializer<Language> serializer;
        private List<Language> languages;

        public LanguageRepository()
        {
            this.serializer = new Serializer<Language>();
            this.languages = this.serializer.FromCSV(filePath);
        }

        public List<Language> GetAll()
        {
            return this.languages;
        }

        public Language? GetById(int id)
        {
            return languages.Find(language => language.Id == id);
        }
        public Language? GetByName(string name)
        {
            return languages.Find(language => language.Name == name);
        }
    }
}
