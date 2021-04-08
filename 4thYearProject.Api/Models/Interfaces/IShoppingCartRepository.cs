﻿namespace _4thYearProject.Api.Models
{
    using _4thYearProject.Shared.Models.BusinessLogic;
    using System.Collections.Generic;

    public interface IShoppingCartRepository
    {
        ShoppingCart AddToCart(string UserId, OrderLineItem olOG);

        ShoppingCart AddOne(string UserId, int PostId);

        ShoppingCart RemoveOne(string UserId, int LineItemId);

        ShoppingCart EmptyBasket(string UserId);

        Order PlaceOrder(string UserId);// get user's basket

        IEnumerable<Order> GetOrders(string UserId);

        ShoppingCart RemoveItem(string UserId, string PostId);

        ShoppingCart GetCart(string UserId);

        ShoppingCart AddCart(ShoppingCart cart);

        Order GetOrderById(int OrderId);

        IEnumerable<OrderLineItem> GetOrderLinesForArtist(string ArtistId);
    }
}
