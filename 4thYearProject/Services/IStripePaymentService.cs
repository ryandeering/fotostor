using _4thYearProject.Shared;
using System.Threading.Tasks;

namespace _4thYearProject.Server.Services
{
    public interface IStripePaymentService
    {
        public Task<SuccessModel> CheckOut(StripePaymentDTO model);
        public Task<SuccessModel> OrderSuccess(string UserId, string token);
    }
}
