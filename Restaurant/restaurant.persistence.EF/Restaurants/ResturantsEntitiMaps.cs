using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using restaurant.Entitis.Restaurants;
using restaurant.Entitis.Users;

namespace restaurants.persistence.EF.Restaurants
{
    public class ResturantsEntitiMaps : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Name).IsRequired();
            builder.Property(r => r.Description).IsRequired();
            builder.Property(r => r.Category).IsRequired();
            builder.Property(r => r.HasDelivery).IsRequired();
            builder.Property(r => r.ContactEmail).IsRequired(false);
            builder.Property(r => r.ContactNumber).IsRequired(false);

            builder.HasMany(r => r.Orders)
                .WithOne(o => o.Restaurant)
                .HasForeignKey(o => o.RestaurantId);

        }
    }
}
