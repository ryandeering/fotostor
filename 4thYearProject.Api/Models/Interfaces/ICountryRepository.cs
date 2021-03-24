namespace _4thYearProject.Api.Models
{
    using _4thYearProject.Shared.Models;
    using System.Collections.Generic;

    public interface ICountryRepository
    {
        IEnumerable<Country> GetAllCountries();

        Country GetCountryById(int countryId);
    }
}
