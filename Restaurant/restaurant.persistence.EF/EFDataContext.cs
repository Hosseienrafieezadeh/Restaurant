using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using restaurant.Entitis.Ap_ication_Users;
using restaurant.Entitis.Orders;
using restaurant.Entitis.Restaurants;
using restaurant.Entitis.Users;
using restaurants.persistence.EF.Restaurants;

namespace restaurants.persistence.EF
{
    public class EFDataContext :IdentityDbContext<ApplicationUser>
    {
        
        //public EFDataContext(string connectionString) :
        //    this(new DbContextOptionsBuilder().UseSqlServer(connectionString).Options)
        //{ }

        public EFDataContext(DbContextOptions<EFDataContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly
                (typeof(EFDataContext).Assembly);
        }
    }
}
