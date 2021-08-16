using System.Collections.Generic;
using System.Threading.Tasks;
using _4thYearProject.Shared.Models;

namespace _4thYearProject.Server.Services
{
    public interface ISuggestionsDataService
    {
        public Task<IEnumerable<Post>> GetSuggestions(string UserId);
    }
}