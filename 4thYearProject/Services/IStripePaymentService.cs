using _4thYearProject.Shared;
using _4thYearProject.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace _4thYearProject.Server.Services
{
    public interface IStripePaymentService
    {
        public Task<SuccessModel> CheckOut(StripePaymentDTO model);
        public Task<SuccessModel> OrderSuccess(string UserId, string token);
    }
}
