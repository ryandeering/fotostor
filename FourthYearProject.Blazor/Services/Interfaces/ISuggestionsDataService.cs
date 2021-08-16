using System.Collections.Generic;
using System.Threading.Tasks;
using FourthYearProject.Shared.Models;

namespace FourthYearProject.Blazor.Services
{
    public interface ISuggestionsDataService
    {
        public Task<IEnumerable<Post>> GetSuggestions(string UserId);
    }
}