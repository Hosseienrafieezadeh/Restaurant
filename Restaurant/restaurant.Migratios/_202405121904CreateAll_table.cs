using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace restaurants.migrations
{
    [Migration(202405121904)]
   public class _202405121904CreateAll_table:Migration
    {
        public override void Up()
        {
            Create.Table("Restaurants")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Description").AsString().NotNullable()
                .WithColumn("Category").AsString().NotNullable()
                .WithColumn("HasDelivery").AsBoolean().NotNullable()
                .WithColumn("ContactEmail").AsString().Nullable()
                .WithColumn("ContactNumber").AsString().Nullable();

            Create.Table("Users")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Username").AsString().NotNullable()
                .WithColumn("Password").AsString().NotNullable()
                .WithColumn("UserType").AsInt32().NotNullable()
                .WithColumn("RestaurantId").AsInt32().Nullable()
                .ForeignKey("FK_Users_Restaurants", "Restaurants", "Id");

            Create.Table("Orders")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("UserId").AsInt32().NotNullable()
                .ForeignKey("FK_Orders_Users", "Users", "Id")
                .WithColumn("RestaurantId").AsInt32().NotNullable()
                .ForeignKey("FK_Orders_Restaurants", "Restaurants", "Id")
                .WithColumn("OrderDate").AsDateTime().NotNullable()
                .WithColumn("TotalAmount").AsDecimal().NotNullable()
                .WithColumn("Notes").AsString().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Orders");
            Delete.Table("Users");
            Delete.Table("Restaurants");
        }
    }
}
