using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _4thYearProject.Shared.Models;

namespace _4thYearProject.Api.Models
{
    public interface ISuggestionsRepository
    {
        IEnumerable<Post> GetSuggestions(string id);




    }
}
