using System.Collections.Generic;
using System.Threading.Tasks;
using FourthYearProject.Shared.Models;

namespace FourthYearProject.Blazor.Services.Interfaces
{
    public interface ISearchDataService
    {
        Task<IEnumerable<SearchResult>> GetSearchResults(string searchResults);
    }
}