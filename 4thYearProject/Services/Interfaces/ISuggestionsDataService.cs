using _4thYearProject.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _4thYearProject.Server.Services
{
    public interface ISuggestionsDataService
    {
        public Task<IEnumerable<Post>> GetSuggestions(string UserId);

    }
}
