using System.Security.Claims;
using System.Threading.Tasks;

namespace FourthYearProject.Shared
{
    public interface IUserService
    {
        Task<ClaimsPrincipal> GetUserAsync();
    }
}