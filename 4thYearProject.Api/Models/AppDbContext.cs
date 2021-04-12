using _4thYearProject.Shared.Models;
using _4thYearProject.Shared.Models.BusinessLogic;
using Microsoft.EntityFrameworkCore;

namespace _4thYearProject.Api.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<JobCategory> JobCategories { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Following> Followers { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Like> Likes { get; set; }

        public DbSet<UserData> Users { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<ShoppingCart> Carts { get; set; }

        public DbSet<OrderLineItem> LineItems { get; set; }

        public DbSet<HashTag> Hashtags { get; set; }

        public DbSet<FeedProfileData> FeedData { get; set; } //to get JSON to serialize...
    }
}