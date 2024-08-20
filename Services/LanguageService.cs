using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Repository;
using BookingApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class LanguageService
    {
        private ILanguageRepository languageRepository;

        public LanguageService(ILanguageRepository languageRepository)
        {
            this.languageRepository = languageRepository;
        }


        public List<Language> GetAll()
        {
            return languageRepository.GetAll();
        }

        public Language GetById(int id)
        {
            return languageRepository.GetById(id);
        }
        public Language GetByName(string name)
        {
            return languageRepository.GetByName(name);
        }
    }
}
