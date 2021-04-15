using _4thYearProject.Shared.Models;
using System.Collections.Generic;

namespace _4thYearProject.Api.Models
{
    public interface ICountryRepository
    {
        IEnumerable<Country> GetAllCountries();

        Country GetCountryById(int countryId);
    }
}