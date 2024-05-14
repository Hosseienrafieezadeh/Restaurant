using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using Restaurants.Contracts.Interface;
using restaurants.persistence.EF;
using restaurants.persistence.EF.Orders;
using restaurants.persistence.EF.Restaurants;
using restaurants.persistence.EF.Users;
using restaurants.Services.Orders;
using restaurants.Services.Orders.Contracts;
using restaurants.Services.Resturants;
using restaurants.Services.Resturants.Contracts;
using restaurants.Services.Users;
using restaurants.Services.Users.Contracts;

var config = GetEnvironment();

var builder = WebApplication
    .CreateBuilder(new WebApplicationOptions
    {
        EnvironmentName = config.GetValue(
            "environment", defaultValue: "Development"),
    });

// Initialize SQLitePCL

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.AddJsonFile("appsettings.json");
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EFDataContext>(
    options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<EFDataContext>();
builder.Services.AddScoped<UnitOfWork, EFUnitOfWork>();
builder.Services.AddScoped<RestaurantService, RestaurantAppService>();
builder.Services.AddScoped<RestaurantRepozitory, EFRestaurantRepozitory>();
builder.Services.AddScoped<OrdersRepozitory, EFOrdersRepozitory>();
builder.Services.AddScoped<OrdersServices, OrdersAppServices>();
builder.Services.AddScoped<UsersRepozitory, EFUsersRepozitory>();
builder.Services.AddScoped<usersService, UserAppService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static IConfigurationRoot GetEnvironment(
    string settingFileName = "appsettings.json")
{
    var baseDirectory = Directory.GetCurrentDirectory();

    return new ConfigurationBuilder()
        .SetBasePath(baseDirectory)
        .AddJsonFile(settingFileName, optional: true, reloadOnChange: true)
        .AddEnvironmentVariables()
        .Build();
}
