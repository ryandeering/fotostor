using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _4thYearProject.Shared;
using _4thYearProject.Shared.Models;

namespace _4thYearProject.Api.Models.Interfaces
{
    public interface ISearchRepository
    {
        IEnumerable<SearchResult> GetSearchResults(string searchText);



    }
}
