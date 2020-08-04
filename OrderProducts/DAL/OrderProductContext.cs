using OrderProducts.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OrderProducts.DAL
{
    public class OrderProductContext : DbContext
    {
        public OrderProductContext() :base("OrderProductContext")
        {
        }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Carousel> Carousels { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<MenuCategory> MenuCategories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<User> Users { get; set; }
    }
}