using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _4thYearProject.Shared.Models;

namespace _4thYearProject.Server.Services.Interfaces
{
  public interface ISearchDataService
{
    Task<IEnumerable<SearchResult>> GetSearchResults(string searchResults);
}
}
