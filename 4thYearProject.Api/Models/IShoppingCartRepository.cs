using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _4thYearProject.Shared.Models;
using _4thYearProject.Shared.Models.BusinessLogic;

namespace _4thYearProject.Api.Models
{
    public interface IShoppingCartRepository
{
        ShoppingCart AddToCart(String UserId, Post post);
        ShoppingCart AddOne(string UserId, int PostId);
        ShoppingCart RemoveOne(string UserId, int LineItemId);
        ShoppingCart EmptyBasket(string UserId);
        Order PlaceOrder(); // get user's basket
        IEnumerable<Order> GetOrders(string UserId);
        ShoppingCart RemoveItem(String UserId, String PostId);
}
}
