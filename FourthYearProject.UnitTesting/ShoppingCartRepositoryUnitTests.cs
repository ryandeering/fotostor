using _4thYearProject.Api.Models;
using _4thYearProject.Shared.Models.BusinessLogic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using _4thYearProject.Shared.Models;
using TestSupport.EfHelpers;
using Xunit;

namespace FourthYearProject.UnitTesting
{
    public class ShoppingCartRepositoryUnitTests
    {





        [Fact]
        public void AddCart_Test()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);
            context.Database.EnsureCreated();
            var repo = new ShoppingCartRepository(context);

            var cart = GenFu.GenFu.New<ShoppingCart>();
            repo.AddCart(cart);

            Assert.Contains(cart, context.Carts);
            context.ChangeTracker.Clear();
            context.Database.EnsureDeleted();
        }


        [Fact]
        public void GetCart_Test()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);
            context.Database.EnsureCreated();
            var repo = new ShoppingCartRepository(context);

            var cart = GenFu.GenFu.New<ShoppingCart>();
            repo.AddCart(cart);
            var cart2 = repo.GetCart(cart.UserId);

            Assert.Equal(cart, cart2);
            context.ChangeTracker.Clear();
            context.Database.EnsureDeleted();
        }

        [Fact]
        public void AddOne_Test()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);
            context.Database.EnsureCreated();
            var repo = new ShoppingCartRepository(context);

            var cart = GenFu.GenFu.New<ShoppingCart>();
            cart.BasketItems = new List<OrderLineItem>();
            var item = GenFu.GenFu.New<OrderLineItem>();
            item.Quantity = 1;
            context.LineItems.Add(item);
            repo.AddCart(cart);
            repo.AddToCart(cart.UserId, item);

            repo.AddOne(cart.UserId, item.Id);

            var hh = repo.GetCart(cart.UserId).BasketItems
                .First(bi => bi.PostId == item.PostId);

            Assert.Equal(2, hh.Quantity);

            context.ChangeTracker.Clear();

        }

        [Fact]
        public void RemoveOne_Test()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);
            context.Database.EnsureCreated();
            var repo = new ShoppingCartRepository(context);

            var cart = GenFu.GenFu.New<ShoppingCart>();
            cart.BasketItems = new List<OrderLineItem>();
            var item = GenFu.GenFu.New<OrderLineItem>();
            item.Quantity = 2;
            context.LineItems.Add(item);
            repo.AddCart(cart);
            repo.AddToCart(cart.UserId, item);

            repo.RemoveOne(cart.UserId, item.Id);

            var hh = repo.GetCart(cart.UserId).BasketItems
                .First(bi => bi.PostId == item.PostId);

            Assert.Equal(1, hh.Quantity);

            context.ChangeTracker.Clear();
            context.Database.EnsureDeleted();
        }


        [Fact]
        public void EmptyBasket_Test()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);
            context.Database.EnsureCreated();
            var repo = new ShoppingCartRepository(context);

            var cart = GenFu.GenFu.New<ShoppingCart>();
            cart.BasketItems = new List<OrderLineItem>();
            var item = GenFu.GenFu.New<OrderLineItem>();
            item.Quantity = 2;
            context.LineItems.Add(item);
            repo.AddCart(cart);
            repo.AddToCart(cart.UserId, item);

            repo.EmptyBasket(cart.UserId);

            var hh = repo.GetCart(cart.UserId);

            Assert.Equal(0, hh.BasketItems.Count);

            context.ChangeTracker.Clear();
            context.Database.EnsureDeleted();
        }


        //[Fact]
        //public void PlaceOrder_Test()
        //{
        //    var options = new DbContextOptionsBuilder<AppDbContext>()
        //        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        //        .Options;

        //    using var context = new AppDbContext(options);
        //    var repo = new ShoppingCartRepository(context);

        //    var cart = GenFu.GenFu.New<ShoppingCart>();
        //    cart.BasketItems = new List<OrderLineItem>();
        //    var item = GenFu.GenFu.New<OrderLineItem>();
        //    item.Quantity = 2;
        //    context.LineItems.Add(item);
        //    repo.AddCart(cart);
        //    repo.AddToCart(cart.UserId, item);

        //    Assert.NotNull(context.Orders.First());
        //    context.ChangeTracker.Clear();
        //    context.Database.EnsureDeleted();

        //}

        [Fact]
        public void PlaceOrder_FAIL_Test()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);
            context.Database.EnsureCreated();
            var repo = new ShoppingCartRepository(context);

            var cart = GenFu.GenFu.New<ShoppingCart>();
            cart.BasketItems = new List<OrderLineItem>();
            var item = GenFu.GenFu.New<OrderLineItem>();
            item.Quantity = 2;
            context.LineItems.Add(item);
            
            var order = repo.PlaceOrder(cart.UserId);

            Assert.Null(order);
            context.ChangeTracker.Clear();
            context.Database.EnsureDeleted();

        }


        [Fact]
        public void AddtoCart_Success_LicenseAlreadyExists_Test()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);
            context.Database.EnsureCreated();
            var repo = new ShoppingCartRepository(context);

            var cart = GenFu.GenFu.New<ShoppingCart>();
            cart.BasketItems = new List<OrderLineItem>();
            var item = GenFu.GenFu.New<OrderLineItem>();
            var post = GenFu.GenFu.New<Post>();
            item.Post = post;
            item.PostId = post.PostId;
            item.Type = "License";
            item.Quantity = 1;
            context.LineItems.Add(item);
            repo.AddCart(cart);
            repo.AddToCart(cart.UserId, item);
            repo.AddToCart(cart.UserId, item);


            var hh = repo.GetCart(cart.UserId);

            Assert.Equal(2, hh.BasketItems.First().Quantity);
            context.ChangeTracker.Clear();
            context.Database.EnsureDeleted();
        }

        [Fact]
        public void AddtoCart_Success_SizeAlreadyExists_Test()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);
            context.Database.EnsureCreated();
            var repo = new ShoppingCartRepository(context);

            var cart = GenFu.GenFu.New<ShoppingCart>();
            cart.BasketItems = new List<OrderLineItem>();
            var item = GenFu.GenFu.New<OrderLineItem>();
            var post = GenFu.GenFu.New<Post>();
            item.Post = post;
            item.PostId = post.PostId;
            item.Type = "Shirt";
            item.Size = "XL";
            item.Quantity = 1;
            context.LineItems.Add(item);
            repo.AddCart(cart);
            repo.AddToCart(cart.UserId, item);
            repo.AddToCart(cart.UserId, item);


            var hh = repo.GetCart(cart.UserId);

            Assert.Equal(2, hh.BasketItems.First().Quantity);
            context.ChangeTracker.Clear();
            context.Database.EnsureDeleted();
        }


        [Fact]
        public void AddtoCart_Success_NoBasketItems_Test()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);
            context.Database.EnsureCreated();
            var repo = new ShoppingCartRepository(context);

            var cart = GenFu.GenFu.New<ShoppingCart>();
            cart.BasketItems = null;
            var item = GenFu.GenFu.New<OrderLineItem>();
            var post = GenFu.GenFu.New<Post>();
            item.Post = post;
            item.PostId = post.PostId;
            item.Type = "Shirt";
            item.Size = "XL";
            item.Quantity = 1;
            context.LineItems.Add(item);
            repo.AddCart(cart);
            repo.AddToCart(cart.UserId, item);


            var hh = repo.GetCart(cart.UserId);

            Assert.NotNull(hh.BasketItems);
            context.ChangeTracker.Clear();
            context.Database.EnsureDeleted();
        }

        [Fact]
        public void GetOrderbyId_Test()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);
            context.Database.EnsureCreated();
            var repo = new ShoppingCartRepository(context);

            var order = GenFu.GenFu.New<Order>();
            order.OrderId = 1;
            order.LineItems = new List<OrderLineItem>();
            var lineitem = GenFu.GenFu.New<OrderLineItem>();
            var post = GenFu.GenFu.New<Post>();
            lineitem.Post = post;
            lineitem.PostId = post.PostId;
            order.LineItems.Add(lineitem);
            context.Orders.Add(order);
            context.SaveChanges();

            var Order = repo.GetOrderById(order.OrderId.GetValueOrDefault());

            Assert.Equal(order.LineItems.Count, Order.LineItems.Count);
            context.ChangeTracker.Clear();
            context.Database.EnsureDeleted();
        }

        [Fact]
        public void GetOrders_Test()
        {
            var options = SqliteInMemory.CreateOptions<AppDbContext>();
            using var context = new AppDbContext(options);

            context.Database.EnsureCreated();
            var repo = new ShoppingCartRepository(context);

            var order = GenFu.GenFu.New<Order>();
            order.OrderId = 100;
            order.LineItems = new List<OrderLineItem>();
            order.OrderAddress = GenFu.GenFu.New<Address>();
            var lineitem = GenFu.GenFu.New<OrderLineItem>();
            var post = GenFu.GenFu.New<Post>();
            lineitem.Post = post;
            lineitem.PostId = post.PostId;
            order.LineItems.Add(lineitem);
            context.Orders.Add(order);
            context.SaveChanges();

            var Order = repo.GetOrders(order.UserId);

            Assert.Equal(order.UserName, Order.First(o => o.UserId == order.UserId).UserName);
            context.ChangeTracker.Clear();
            context.Database.EnsureDeleted();
        }

        [Fact]
        public void GetOrderLinesForArtist_Test()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);
            var repo = new ShoppingCartRepository(context);

            var order = GenFu.GenFu.New<Order>();
            order.OrderId = 2323;
            order.LineItems = new List<OrderLineItem>();
            order.OrderAddress = GenFu.GenFu.New<Address>();
            var lineitem = GenFu.GenFu.New<OrderLineItem>();
            var post = GenFu.GenFu.New<Post>();
            lineitem.Post = post;
            lineitem.PostId = post.PostId;
            order.LineItems.Add(lineitem);
            context.Orders.Add(order);
            context.SaveChanges();

            var Order = repo.GetOrderLinesForArtist(post.UserId);

            Assert.Equal(post.UserId,Order.First().Post.UserId);
            context.Database.EnsureDeleted();

        }






    }
}
