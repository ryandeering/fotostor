using System.Threading.Tasks;
using _4thYearProject.Shared;
using _4thYearProject.Shared.Models.BusinessLogic;

namespace _4thYearProject.Server.Services
{
    public interface IStripePaymentService
    {
        public Task<SuccessModel> CheckOut(StripePaymentDto model);
        public Task<SuccessModel> OrderSuccess(string UserId, string token);
    }
}