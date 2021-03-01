﻿using _4thYearProject.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace _4thYearProject.Server.Services
{
    public class StripePaymentService : IStripePaymentService
{
    private readonly HttpClient _client;

    public StripePaymentService(HttpClient client)
    {
        _client = client;
    }
    public async Task<SuccessModel> CheckOut(StripePaymentDTO model)
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
        else
        {
            var contentTemp = await response.Content.ReadAsStringAsync();
            var errorModel = JsonConvert.DeserializeObject<ErrorModel>(contentTemp);
            throw new Exception(errorModel.ErrorMessage);
        }
    }

        public async Task<HttpContent> OrderSuccess(string UserId, string token)
        {
            var response = await _client.GetAsync($"api/stripepayment/OrderSuccess/{UserId}/{token}");

            return response.Content;

        }
    }
}
