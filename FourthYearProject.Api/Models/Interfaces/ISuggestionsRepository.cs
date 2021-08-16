using System.Collections.Generic;
using FourthYearProject.Shared.Models;

namespace FourthYearProject.Api.Models
{
    public interface ISuggestionsRepository
    {
        IEnumerable<Post> GetSuggestions(string id);
    }
}