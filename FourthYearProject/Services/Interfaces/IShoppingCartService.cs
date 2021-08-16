using System.Collections.Generic;
using System.Threading.Tasks;
using _4thYearProject.Shared.Models.BusinessLogic;

namespace _4thYearProject.Server.Services
{
    public interface IShoppingCartService
    {
        Task<IEnumerable<Order>> GetAllOrders(string UserId);
        Task<ShoppingCart> AddToCart(string UserId, OrderLineItem olOG);
        Task RemoveOne(string UserId, OrderLineItem lineItem);
        Task EmptyBasket(string UserId);
        Task<ShoppingCart> AddCart(string UserId);
        Task<ShoppingCart> GetCart(string UserId);
        Task<Order> GetOrderById(int OrderID);
        Task<IEnumerable<OrderLineItem>> GetOrderLinesForArtist(string ArtistId);
        Task AddOne(string UserId, OrderLineItem lineItem);
    }
}