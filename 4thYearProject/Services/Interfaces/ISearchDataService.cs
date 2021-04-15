using _4thYearProject.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _4thYearProject.Server.Services.Interfaces
{
    public interface ISearchDataService
    {
        Task<IEnumerable<SearchResult>> GetSearchResults(string searchResults);
    }
}
