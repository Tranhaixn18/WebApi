using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;//sử dụng để đọc file appsetting.json
using System.IO;//thao tác với các file
using Microsoft.EntityFrameworkCore;


namespace webMobile.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options) { }
      
        public DbSet<UsersRecord> Users { get; set; }
        public DbSet<CategoriesRecord> Categories { get; set; }
        public DbSet<ProductsRecord> Products { get; set; }
        public DbSet<RatingRecord> Rating { get; set; }
        public DbSet<CustomersRecord> Customers { get; set; }
        public DbSet<OrdersRecord> Orders { get; set; }
        public DbSet<OrdersDetailRecord> OrdersDetail { get; set; }
        public DbSet<RoleRecord> Role { get; set; }
        public DbSet<RoleActionRecord> RoleAction { get; set; }
        public DbSet<NewsRecord> News { get; set; }
        public DbSet<ProductImageRecord> ProductImageRecords { get; set; }
        public DbSet<CartRecord> CartRecords { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

    }
}
