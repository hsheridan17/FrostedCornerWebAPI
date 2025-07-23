using AutoMapper;
using FrostedCornerWebAPI.Data;
using FrostedCornerWebAPI.Models;
using FrostedCornerWebAPI.Services.SuppliesService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrostedCornerWebAPI.Test
{
    public class SuppliesTest
    {
        private SuppliesService CreateServiceWithSeededDb()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var context = new DataContext(options);

            // Seed Franchises
            context.Franchises.AddRange(
                new Franchise { FranchiseId = 1, FranchiseName = "Franchise1", Address = "Address for Franchise1" },
                new Franchise { FranchiseId = 2, FranchiseName = "Franchise2", Address = "Address for Franchise2" }
            );

            // Seed Items
            context.Items.AddRange(
                new Item { ItemId = 1, Name = "Item1", Price = 12.99f, Description = "Description for Item1" },
                new Item { ItemId = 2, Name = "Item2", Price = 10.99f, Description = "Description for Item2" }
            );

            // Seed SuppliesOrders
            var suppliesOrder1 = new SuppliesOrder
            {
                SuppliesOrderId = 1,
                FranchiseId = 1,
                Total = 23.98f,
                Time = DateTime.UtcNow,
                SuppliesItems = new List<SuppliesItem>()
            };

            // Seed SuppliesItems
            var suppliesItem1 = new SuppliesItem
            {
                SuppliesItemId = 1,
                OrderId = 1,
                ItemId = 1,
                Quantity = 1,
                SubTotal = 12.99f
            };
            var suppliesItem2 = new SuppliesItem
            {
                SuppliesItemId = 2,
                OrderId = 1,
                ItemId = 2,
                Quantity = 1,
                SubTotal = 10.99f
            };

            suppliesOrder1.SuppliesItems.Add(suppliesItem1);
            suppliesOrder1.SuppliesItems.Add(suppliesItem2);

            context.SuppliesOrders.Add(suppliesOrder1);
            context.SuppliesItems.AddRange(suppliesItem1, suppliesItem2);

            context.SaveChanges();

            // Add AutoMapper configuration as needed
            var config = new MapperConfiguration(cfg => cfg.AddProfile<FrostedCornerWebAPI.AutoMapperProfile>());
            var mapper = config.CreateMapper();

            return new SuppliesService(mapper, context);
        }

        [Fact]
        public async Task TestGetAllSupplies()
        {
            var suppliesService = CreateServiceWithSeededDb();
            var result = await suppliesService.GetAllSuppliesOrders();
            Assert.NotNull(result.Data);
            Assert.Single(result.Data);
            var order = result.Data.First();
            Assert.Equal(23.98f, order.Total);
        }
    }
}
