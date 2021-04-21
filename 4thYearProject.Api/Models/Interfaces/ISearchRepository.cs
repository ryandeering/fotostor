﻿using System.Collections.Generic;
using _4thYearProject.Shared.Models;

namespace _4thYearProject.Api.Models.Interfaces
{
    public interface ISearchRepository
    {
        IEnumerable<SearchResult> GetSearchResults(string searchText);
    }
}