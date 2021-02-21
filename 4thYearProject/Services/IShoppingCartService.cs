using _4thYearProject.Shared.Models;
using _4thYearProject.Shared.Models.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4thYearProject.Server.Services
{
    public interface IShoppingCartService
{
    Task<IEnumerable<Order>> GetAllOrders(string UserId);
    Task<ShoppingCart> AddToCart(string UserId, int PostId);
    Task AddOne(string UserId, Post post);
    Task RemoveOne(string UserId, OrderLineItem lineItem);
    Task EmptyBasket(string UserId);
   // Task<ShoppingCart> RemoveItem(String UserId, String PostId);
    }
}
