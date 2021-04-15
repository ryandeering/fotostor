using _4thYearProject.Shared.Models.BusinessLogic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _4thYearProject.Api.Models
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly AppDbContext _appDbContext;

        public ShoppingCartRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public ShoppingCart AddToCart(string UserId, OrderLineItem olOG)
        {
            var cart = _appDbContext.Carts
                .Where(x => x.UserId == UserId)
                .Include(x => x.BasketItems)
                .ThenInclude(x => x.Post)
                .FirstOrDefault();

            var itemFound = false;

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
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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

        public IEnumerable<OrderLineItem> GetOrderLinesForArtist(string ArtistId)
        {
            return _appDbContext.Orders.Include("LineItems.Post").SelectMany(o => o.LineItems)
                .Where(ol => ol.Post.UserId == ArtistId);
        }


        public Order GetOrderById(int OrderId)
        {
            return _appDbContext.Orders.Where(o => o.OrderId.Equals(OrderId)).Include("OrderAddress").Include(o => o.LineItems)
                .ThenInclude(ol => ol.Post).First();
        }

        public Order PlaceOrder(string UserId)
        {
            var result = _appDbContext.Carts
                .Where(x => x.UserId == UserId)
                .Include(x => x.BasketItems)
                .ThenInclude(x => x.Post)
                .FirstOrDefault();

            var User = _appDbContext.Users.Where(u => u.Id == UserId).Include("Address").FirstOrDefault();


            if (result == null) return null;

            var order = new Order();

            try
            {
                order.UserId = UserId;
                var items = new List<OrderLineItem>(result.BasketItems);
                //set address
                order.DatePlaced = DateTime.Now;
                order.LineItems = items;
                order.OrderAddress = User.Address;
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
            var cart = _appDbContext.Carts.Include("BasketItems").FirstOrDefault(c => c.UserId == UserId);
            var temp = cart.BasketItems.First(i => i.Id == LineItemId);
            if (temp.Quantity > 1)
            {
                temp.Quantity--;
            }
            else
            {
                cart.BasketItems.Remove(temp);
            }
            _appDbContext.SaveChanges();
            return cart;
        }


        public ShoppingCart AddOne(string UserId, int LineItemId)
        {
            var cart = _appDbContext.Carts.Include("BasketItems").FirstOrDefault(c => c.UserId == UserId);
            var temp = cart.BasketItems.First(i => i.Id == LineItemId);
            temp.Quantity++;
            _appDbContext.SaveChanges();
            return cart;
        }

        public ShoppingCart GetCart(string UserId)
        {
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