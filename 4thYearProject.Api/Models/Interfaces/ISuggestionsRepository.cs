using _4thYearProject.Shared.Models;
using System.Collections.Generic;

namespace _4thYearProject.Api.Models
{
    public interface ISuggestionsRepository
    {
        IEnumerable<Post> GetSuggestions(string id);
    }
}