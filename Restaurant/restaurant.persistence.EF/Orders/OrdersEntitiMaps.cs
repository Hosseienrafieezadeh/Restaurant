using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using restaurant.Entitis.Orders;
using restaurant.Entitis.Restaurants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurants.persistence.EF.Orders
{
    public class OrdersEntitiMaps: IEntityTypeConfiguration<Order>
    {
       

            public void Configure(EntityTypeBuilder<Order> builder)
            {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.OrderDate).IsRequired();
            builder.Property(o => o.TotalAmount).IsRequired();
            builder.Property(o => o.Notes).IsRequired();

            builder.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            builder.HasOne(o => o.Restaurant)
                .WithMany(r => r.Orders)
                .HasForeignKey(o => o.RestaurantId);
        }
        }

}
    

