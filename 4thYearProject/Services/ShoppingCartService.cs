using _4thYearProject.Shared.Models;
using _4thYearProject.Shared.Models.BusinessLogic;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace _4thYearProject.Server.Services
{
    public class ShoppingCartDataService : IShoppingCartService
    {
        private readonly HttpClient _httpClient;

        public ShoppingCartDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ShoppingCart> AddToCart(string UserId, Post post)
        {
            var cartJson =
                new StringContent(JsonSerializer.Serialize(post), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/shoppingcart/add/{UserId}/", cartJson);

           if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<ShoppingCart>(await response.Content.ReadAsStreamAsync());
           }

            return null;
        }

        public async Task AddOne(string UserId, Post post)
        {
            var cartJson =
                new StringContent(JsonSerializer.Serialize(post), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/shoppingcart/add/incre/{UserId}/", cartJson);
        }

        public async Task RemoveOne(string UserId, OrderLineItem lineItem)
        {
            var cartJson =
                new StringContent(JsonSerializer.Serialize(lineItem), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/shoppingcart/add/remove/{UserId}/", cartJson);
        }


        public async Task EmptyBasket(string UserId)
        {
            await _httpClient.DeleteAsync($"api/shoppingcart/empty/{UserId}");
        }

        public async Task<IEnumerable<Order>> GetAllOrders(string UserId)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Order>>
                    (await _httpClient.GetStreamAsync($"api/shoppingcart/orders/{UserId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }


        public Task<ShoppingCart> AddToCart(string UserId, int PostId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ShoppingCart> AddOne(string UserId, int LineItemId)
        {
            var response = await _httpClient.PutAsync("api/shoppingcart/add/incre/{UserId}/{LineItemId}", null) ;

                return await JsonSerializer.DeserializeAsync<ShoppingCart>(await response.Content.ReadAsStreamAsync());


        }

    }
}
