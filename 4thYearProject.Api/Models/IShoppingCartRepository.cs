﻿using _4thYearProject.Shared.Models.BusinessLogic;
using System;
using System.Collections.Generic;

namespace _4thYearProject.Api.Models
{
    public interface IShoppingCartRepository
    {
        ShoppingCart AddToCart(String UserId, OrderLineItem olOG);
        ShoppingCart AddOne(string UserId, int PostId);
        ShoppingCart RemoveOne(string UserId, int LineItemId);
        ShoppingCart EmptyBasket(string UserId);
        Order PlaceOrder(string UserId); // get user's basket
        IEnumerable<Order> GetOrders(string UserId);
        ShoppingCart RemoveItem(String UserId, String PostId);
        ShoppingCart GetCart(string UserId);
        ShoppingCart AddCart(ShoppingCart cart);
        Order GetOrderById(int OrderId);
    }
}
