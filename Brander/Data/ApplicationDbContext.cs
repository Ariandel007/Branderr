using System;
using System.Collections.Generic;
using System.Text;
using Brander.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Brander.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //cada vez que añadamos algo al modelo aqui tambien debe ser añadido para que sea añadido a la base de datos.
        //para añairlos nos vamos a la consola de NuGet: add-migration "Nombre Lo mas descripctivo posible"
        //despues en la misma consola escribimos update-database

        public DbSet<Category> Category { get; set; }

        public DbSet<SubCategory> SubCategory { get; set; }

        public DbSet<Game> Game { get; set; }

        public DbSet<Comment> Comment { get; set; }

        public DbSet<Supplier> Supplier { get; set; }

        public DbSet<Stock> Stock { get; set; }

        public DbSet<Coupon> Coupon { get; set; }

        public DbSet<Order> Order { get; set; }

        public DbSet<Key> Key { get; set; }

        public DbSet<OrderDetails> OrderDetails { get; set; }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; internal set; }
    }
}
