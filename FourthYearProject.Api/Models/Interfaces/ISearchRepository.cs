using System.Collections.Generic;
using FourthYearProject.Shared.Models;

namespace FourthYearProject.Api.Models.Interfaces
{
    public interface ISearchRepository
    {
        IEnumerable<SearchResult> GetSearchResults(string searchText);
    }
}