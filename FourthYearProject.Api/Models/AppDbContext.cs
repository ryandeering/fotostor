using FourthYearProject.Shared.Models;
using FourthYearProject.Shared.Models.BusinessLogic;
using Microsoft.EntityFrameworkCore;

namespace FourthYearProject.Api.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Following> Followers { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Like> Likes { get; set; }

        public DbSet<UserData> Users { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<ShoppingCart> Carts { get; set; }

        public DbSet<OrderLineItem> LineItems { get; set; }

        public DbSet<HashTag> Hashtags { get; set; }

        public DbSet<FeedProfileData> FeedData { get; set; }
    }
}