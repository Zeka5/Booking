﻿using BookingApp.Domain.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ILanguageRepository
    {
        List<Language> GetAll();
        Language? GetById(int id);
        Language? GetByName(string name);

    }
}
