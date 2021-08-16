using System.Threading.Tasks;
using FourthYearProject.Shared;
using FourthYearProject.Shared.Models.BusinessLogic;

namespace FourthYearProject.Blazor.Services
{
    public interface IStripePaymentService
    {
        public Task<SuccessModel> CheckOut(StripePaymentDto model);
        public Task<SuccessModel> OrderSuccess(string UserId, string token);
    }
}