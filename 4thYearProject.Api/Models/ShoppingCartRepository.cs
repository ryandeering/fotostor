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

        public ShoppingCart AddToCart(String UserId, OrderLineItem olOG)
        {
            ShoppingCart cart = _appDbContext.Carts.Where(c => c.UserId == UserId).Include(c => c.basketItems).FirstOrDefault();

            Boolean itemFound = false;

            //TODO, validate the item is actually possible to be added

            if (cart.basketItems == null)
            {
                cart.basketItems = new List<OrderLineItem>(); ;
            }
            else
            {
                foreach (OrderLineItem ol in cart.basketItems)
                {

                    if (ol.Post.PostId.Equals(olOG.Post.PostId) && ol.Type.GetType() == olOG.Type.GetType())
                    {

                        ol.Quantity++;
                        itemFound = true;
                        break;

                    }

                }

                if (!itemFound)
                {
                    cart.basketItems.Add(olOG);
                }
            }

            cart.basketItems.Add(olOG);
            _appDbContext.Carts.Update(cart);
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
            cart.basketItems.Clear();
            _appDbContext.SaveChanges();
            return cart;
        }

        public IEnumerable<Order> GetOrders(string UserId)
        {
            return _appDbContext.Orders.Where(o => o.UserId.Equals(UserId));
        }

        public Order PlaceOrder(string UserId)
        {
            var result = _appDbContext.Carts
      .Where(x => x.UserId == UserId)
      .Include(x => x.basketItems)
      .ThenInclude(x => x.Post)
      .FirstOrDefault();


            Order order = new Order();


            try
            {
                order.UserId = UserId;
                List<OrderLineItem> items = new List<OrderLineItem>(result.basketItems);
                //set address
                order.DatePlaced = DateTime.Now;
                order.LineItems = items;
            } catch (Exception e)
            {
                Console.WriteLine(e);
            }

            _appDbContext.Orders.Add(order);
            result.basketItems.Clear();
            _appDbContext.SaveChanges();










            return new Order();
        }

        public ShoppingCart RemoveOne(string UserId, int LineItemId)
        {
            ShoppingCart cart = _appDbContext.Carts.FirstOrDefault(c => c.UserId == UserId);
            OrderLineItem temp = cart.basketItems.First(i => i.Id == LineItemId);
            cart.basketItems.Remove(temp);
            _appDbContext.SaveChanges();
            return cart;
        }

        public ShoppingCart GetCart(string UserId)
        {
            //return _appDbContext.Carts.Where(c => c.UserId == UserId).Include(c => c.basketItems).Where(p => p.).FirstOrDefault();
            var result = _appDbContext.Carts
       .Where(x => x.UserId == UserId)
       .Include(x => x.basketItems)
       .ThenInclude(x => x.Post)
       .SingleOrDefault();

            return result;
        }

        public ShoppingCart AddCart(ShoppingCart cart)
        {
            cart.basketItems = new List<OrderLineItem>();
            _appDbContext.Carts.Add(cart);
            _appDbContext.SaveChanges();
            return cart;
        }




    }
}
