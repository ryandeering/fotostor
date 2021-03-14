namespace _4thYearProject.Api.Models
{
    using _4thYearProject.Shared.Models.BusinessLogic;
    using Microsoft.EntityFrameworkCore;
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

            var cart = _appDbContext.Carts.FirstOrDefault(c => c.UserId == UserId);

            //TODO, validate the item is actually possible to be added

            foreach (var ol in cart.BasketItems)
                if (ol.Post.PostId.Equals(PostId))
                    //TODO validation for type
                    ol.Quantity++;
            _appDbContext.SaveChanges();
            return cart;
        }

        public ShoppingCart AddToCart(string UserId, OrderLineItem olOG)
        {
            var cart = _appDbContext.Carts
                .Where(x => x.UserId == UserId)
                .Include(x => x.BasketItems)
                .ThenInclude(x => x.Post)
                .FirstOrDefault();

            var itemFound = false;

            //TODO, validate the item is actually possible to be added

            if (cart.BasketItems == null)
            {
                cart.BasketItems = new List<OrderLineItem>();
            }
            else
            {
                foreach (var ol in cart.BasketItems)
                    if (ol.Post.PostId.Equals(olOG.Post.PostId) && ol.Type == olOG.Type)
                    {
                        if (ol.Type == "License")
                        {
                            ol.Quantity++;
                            itemFound = true;
                            break;
                        }

                        if (ol.Size.Equals(olOG.Size))
                        {
                            ol.Quantity++;
                            itemFound = true;
                            break;
                        }
                    }

                if (!itemFound) cart.BasketItems.Add(olOG);
            }

            try
            {
                _appDbContext.Entry(cart).CurrentValues.SetValues(cart);
                // _appDbContext.Carts.Update(cart);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            _appDbContext.SaveChanges();
            return cart;
        }

        public ShoppingCart RemoveItem(string UserId, string PostId)
        {
            //TODO validate if user is the intended user

            var cart = _appDbContext.Carts.FirstOrDefault(c => c.UserId == UserId);

            //TODO, validate the item is actually possible to be added

            foreach (var ol in cart.BasketItems)
                if (ol.Post.PostId.Equals(PostId))
                {
                    //TODO validation for type

                    if (ol.Quantity > 1)
                    {
                        ol.Quantity--;
                    }
                    else
                    {
                        cart.BasketItems.Remove(ol);
                        break;
                    }
                }

            _appDbContext.SaveChanges();
            return cart;
        }

        public ShoppingCart EmptyBasket(string UserId)
        {
            var cart = _appDbContext.Carts
                .Where(x => x.UserId == UserId)
                .Include(x => x.BasketItems)
                .FirstOrDefault();
            cart.BasketItems.Clear();
            _appDbContext.SaveChanges();
            return cart;
        }

        public IEnumerable<Order> GetOrders(string UserId)
        {
            return _appDbContext.Orders.Where(o => o.UserId.Equals(UserId));
        }

        public Order GetOrderById(int OrderId)
        {
            return _appDbContext.Orders.Where(o => o.OrderId.Equals(OrderId)).Include(o => o.LineItems)
                .ThenInclude(ol => ol.Post).First();
        }

        public Order PlaceOrder(string UserId)
        {
            var result = _appDbContext.Carts
                .Where(x => x.UserId == UserId)
                .Include(x => x.BasketItems)
                .ThenInclude(x => x.Post)
                .FirstOrDefault();

            var order = new Order();

            try
            {
                order.UserId = UserId;
                var items = new List<OrderLineItem>(result.BasketItems);
                //set address
                order.DatePlaced = DateTime.Now;
                order.LineItems = items;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            _appDbContext.Orders.Add(order);
            result.BasketItems.Clear();
            _appDbContext.SaveChanges();

            return order;
        }

        public ShoppingCart RemoveOne(string UserId, int LineItemId)
        {
            var cart = _appDbContext.Carts.FirstOrDefault(c => c.UserId == UserId);
            var temp = cart.BasketItems.First(i => i.Id == LineItemId);
            cart.BasketItems.Remove(temp);
            _appDbContext.SaveChanges();
            return cart;
        }

        public ShoppingCart GetCart(string UserId)
        {
            //return _appDbContext.Carts.Where(c => c.UserId == UserId).Include(c => c.basketItems).Where(p => p.).FirstOrDefault();
            var result = _appDbContext.Carts
                .Where(x => x.UserId == UserId)
                .Include(x => x.BasketItems)
                .ThenInclude(x => x.Post)
                .SingleOrDefault();

            return result;
        }

        public ShoppingCart AddCart(ShoppingCart cart)
        {
            cart.BasketItems = new List<OrderLineItem>();
            _appDbContext.Carts.Add(cart);
            _appDbContext.SaveChanges();
            return cart;
        }

        public IEnumerable<OrderLineItem> GetOrderLinesForUser(string UserId)
        {
            var lineitems = _appDbContext.LineItems.Where(ol => ol.Post.UserId.Contains(UserId)).ToList();

            return lineitems;
        }
    }
}
