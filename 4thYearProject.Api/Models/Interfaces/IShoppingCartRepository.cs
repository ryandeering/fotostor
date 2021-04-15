﻿using _4thYearProject.Shared.Models.BusinessLogic;
using System.Collections.Generic;

namespace _4thYearProject.Api.Models
{
    public interface IShoppingCartRepository
    {
        ShoppingCart AddToCart(string UserId, OrderLineItem olOG);

        ShoppingCart RemoveOne(string UserId, int LineItemId);

        ShoppingCart EmptyBasket(string UserId);

        Order PlaceOrder(string UserId); 

        IEnumerable<Order> GetOrders(string UserId);

        ShoppingCart AddOne(string UserId, int LineItemId);

        ShoppingCart GetCart(string UserId);

        ShoppingCart AddCart(ShoppingCart cart);

        Order GetOrderById(int OrderId);

        IEnumerable<OrderLineItem> GetOrderLinesForArtist(string ArtistId);
    }
}