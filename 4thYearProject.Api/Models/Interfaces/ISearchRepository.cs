using _4thYearProject.Shared.Models;
using System.Collections.Generic;

namespace _4thYearProject.Api.Models.Interfaces
{
    public interface ISearchRepository
    {
        IEnumerable<SearchResult> GetSearchResults(string searchText);
    }
}