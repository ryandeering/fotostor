using _4thYearProject.Shared.Models;
using _4thYearProject.Shared.Models.BusinessLogic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _4thYearProject.Server.Services
{
    public interface IShoppingCartService
    {
        Task<IEnumerable<Order>> GetAllOrders(string UserId);
        Task<ShoppingCart> AddToCart(string UserId, OrderLineItem olOG);
        Task AddOne(string UserId, Post post);
        Task RemoveOne(string UserId, OrderLineItem lineItem);
        Task EmptyBasket(string UserId);
        Task<ShoppingCart> AddCart(string UserId);
        Task<ShoppingCart> GetCart(string UserId);
        Task<Order> GetOrderById(int OrderID);
        // Task<ShoppingCart> RemoveItem(String UserId, String PostId);
    }
}
