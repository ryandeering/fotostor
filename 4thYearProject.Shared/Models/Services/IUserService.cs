using System.Security.Claims;
using System.Threading.Tasks;

namespace _4thYearProject.Shared
{
    public interface IUserService
    {
        Task<ClaimsPrincipal> GetUserAsync();
    }
}