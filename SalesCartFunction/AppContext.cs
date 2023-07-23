using Microsoft.EntityFrameworkCore;
using SalesCartFunction.Models;

namespace SalesCartFunction
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ShoopingCart> ShoppingCarts { get; set; }
        public virtual DbSet<ShoopingCartDetails> ShoppingCartDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


        }

    }
}

