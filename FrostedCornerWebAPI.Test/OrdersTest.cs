using FrostedCornerWebAPI.Data;
using FrostedCornerWebAPI.Models;
using FrostedCornerWebAPI.Services.OrderService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrostedCornerWebAPI.Test
{
    public class OrdersTest
    {
        //private OrderService CreateServiceWithSeededDb()
        //{
        //    var options = new DbContextOptionsBuilder<DataContext>()
        //        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique DB per test
        //        .Options;
        //    var context = new DataContext(options);
        //    // Seed the database with some orders
        //    context.Orders.AddRange(
        //        new Order { OrderId = 1, Name = "John Doe", TotalAmount = 29.99f },
        //        new Order { OrderId = 2, Name = "Jane Smith", TotalAmount = 49.99f },
        //        new Order { OrderId = 3, CustomerName = "Alice Johnson", TotalAmount = 19.99f },
        //        new Order { OrderId = 4, CustomerName = "Bob Brown", TotalAmount = 39.99f }
        //    );
        //    context.SaveChanges();
        //    var config = new MapperConfiguration(cfg => cfg.AddProfile<FrostedCornerWebAPI.AutoMapperProfile>());
        //    var mapper = config.CreateMapper();
        //    return new OrderService(mapper, context);
        //}


        //[Fact]
        //public async Task TestGetAllOrdersAsync()
        //{
        //    // Arrange
        //    var orderService = CreateServiceWithSeededDb();
            
        //    // Act
        //    var result = await orderService.GetAllOrders();
            
        //    // Assert
        //    Assert.NotNull(result.Data);
        //    Assert.Equal(4, result.Data.Count);
        //    Assert.Contains(result.Data, order => order.OrderId == 1);
        //    Assert.Contains(result.Data, order => order.OrderId == 2);
        //}
    }
}
