namespace _4thYearProject.Api.Models
{
    using _4thYearProject.Shared.Models;
    using _4thYearProject.Shared.Models.BusinessLogic;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly AppDbContext _appDbContext;

        public ShoppingCartRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public ShoppingCart AddOne(string UserId, int PostId)
        {
            //TODO validate if user is the intended user

            ShoppingCart cart = _appDbContext.Carts.FirstOrDefault(c => c.UserId == UserId);


            //TODO, validate the item is actually possible to be added

            foreach (OrderLineItem ol in cart.basketItems)
            {

                if (ol.Post.PostId.Equals(PostId))
                {
                    //TODO validation for type
                    ol.Quantity++;
                }

            }
            _appDbContext.SaveChanges();
            return cart;
        }

        public ShoppingCart AddToCart(String UserId, Post post)
        {
            ShoppingCart cart = _appDbContext.Carts.FirstOrDefault(c => c.UserId == UserId);

            Boolean itemFound = false;

            //TODO, validate the item is actually possible to be added



            foreach (OrderLineItem ol in cart.basketItems)
            {

                if (ol.Post.PostId.Equals(post.PostId)) //TODO, something for type of item here.
                {

                    ol.Quantity++;
                    itemFound = true;
                    break;

                }

            }

            if (!itemFound)
            {

                OrderLineItem NewItem = new OrderLineItem(post);
                cart.basketItems.Add(NewItem);
            }

            _appDbContext.SaveChanges();
            return cart;
        }

        public ShoppingCart RemoveItem(String UserId, String PostId)
        {

            //TODO validate if user is the intended user




            ShoppingCart cart = _appDbContext.Carts.FirstOrDefault(c => c.UserId == UserId);


            //TODO, validate the item is actually possible to be added



            foreach (OrderLineItem ol in cart.basketItems)
            {

                if (ol.Post.PostId.Equals(PostId))
                {

                    //TODO validation for type

                    if (ol.Quantity > 1)
                    {
                        ol.Quantity--;
                    }
                    else
                    {
                        cart.basketItems.Remove(ol);
                        break;
                    }
                }

            }
            _appDbContext.SaveChanges();
            return cart;
        }

        public ShoppingCart EmptyBasket(String UserId)
        {
            ShoppingCart cart = _appDbContext.Carts.FirstOrDefault(c => c.UserId == UserId);
            cart = new ShoppingCart();
            _appDbContext.SaveChanges();
            return cart;
        }

        public IEnumerable<Order> GetOrders(string UserId)
        {
            return _appDbContext.Orders.Where(o => o.UserId.Equals(UserId));
        }

        public Order PlaceOrder()
        {
            throw new NotImplementedException();
        }

        public ShoppingCart RemoveOne(string UserId, int LineItemId)
        {
            ShoppingCart cart = _appDbContext.Carts.FirstOrDefault(c => c.UserId == UserId);
            OrderLineItem temp = cart.basketItems.First(i => i.Id == LineItemId);
            cart.basketItems.Remove(temp);
            _appDbContext.SaveChanges();
            return cart;
        }
    }
}
