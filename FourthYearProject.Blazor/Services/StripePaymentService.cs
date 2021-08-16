using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FourthYearProject.Shared;
using FourthYearProject.Shared.Models.BusinessLogic;
using Newtonsoft.Json;

namespace FourthYearProject.Blazor.Services
{
    public class StripePaymentService : IStripePaymentService
    {
        private readonly HttpClient _client;

        public StripePaymentService(HttpClient client)
        {
            _client = client;
        }

        public async Task<SuccessModel> CheckOut(StripePaymentDto model)
        {
            var content = JsonConvert.SerializeObject(model);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/stripepayment/create", bodyContent);

            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<SuccessModel>(contentTemp);
                return result;
            }

            return null;
        }

        public async Task<SuccessModel> OrderSuccess(string UserId, string token)
        {
            var response = await _client.GetStringAsync($"api/stripepayment/OrderSuccess/{UserId}/{token}");
            var result = JsonConvert.DeserializeObject<SuccessModel>(response);
            return result;
        }
    }
}