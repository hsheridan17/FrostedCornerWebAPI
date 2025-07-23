using AutoMapper;
using FrostedCornerWebAPI.Data;
using FrostedCornerWebAPI.Models;
using FrostedCornerWebAPI.Services.ItemService;
using Microsoft.EntityFrameworkCore;

namespace FrostedCornerWebAPI.Test
{
    public class ItemsTest
    {
        private ItemService CreateServiceWithSeededDb()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique DB per test
                .Options;
            var context = new DataContext(options);

            context.Items.Add(new Item { ItemId = 1, Name = "TestItem1", Price = 2.99f, Description = "Test Description" });
            context.Items.Add(new Item { ItemId = 2, Name = "TestItem2", Price = 5.99f, Description = "Test Description" });
            context.Items.Add(new Item { ItemId = 3, Name = "TestItem3", Price = 12.99f, Description = "Test Description" });
            context.Items.Add(new Item { ItemId = 4, Name = "TestItem4", Price = 22.99f, Description = "Test Description" });
            context.SaveChanges();

            var config = new MapperConfiguration(cfg => cfg.AddProfile<FrostedCornerWebAPI.AutoMapperProfile>());
            var mapper = config.CreateMapper();

            return new ItemService(mapper, context);
        }

        [Fact]
        public async Task TestGetAllItemsAsync_withItems()
        {
            var itemService = CreateServiceWithSeededDb();
            var result = await itemService.GetAllItems();
            Assert.NotNull(result.Data);
            Assert.Equal(4, result.Data.Count);
            Assert.Contains(result.Data, item => item.Name == "TestItem1");
            Assert.Contains(result.Data, item => item.Name == "TestItem2");
        }

        [Fact]
        public async Task TestGetItemByIdAsync_withValidId()
        {
            var itemService = CreateServiceWithSeededDb();
            var result = await itemService.GetItemById(1);
            Assert.NotNull(result.Data);
            Assert.Equal("TestItem1", result.Data.Name);
        }

        [Fact]
        public async Task TestGetItemByIdAsync_withInvalidId()
        {
            var itemService = CreateServiceWithSeededDb();
            var result = await itemService.GetItemById(999);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal("Item not found.", result.Message);
        }

        [Fact]
        public async Task TestAddItemAsync()
        {
            var itemService = CreateServiceWithSeededDb();
            var newItem = new FrostedCornerWebAPI.Dtos.Item.AddItemDto
            {
                Name = "NewItem5",
                Price = 10.99f,
                Description = "New Item Description"
            };
            var result = await itemService.AddItem(newItem);
            Assert.NotNull(result.Data);
            Assert.Equal(5, result.Data.Count);
            Assert.Contains(result.Data, item => item.Name == "NewItem5");
        }

        [Fact]
        public async Task TestRemoveItemByIdAsync_withValidId()
        {
            var itemService = CreateServiceWithSeededDb();
            var result = await itemService.RemoveItemById(1);
            Assert.NotNull(result.Data);
            Assert.Equal(3, result.Data.Count); // One item should be removed
            Assert.DoesNotContain(result.Data, item => item.Name == "TestItem1");
        }

        [Fact]
        public async Task TestRemoveItemByIdAsync_withInvalidId()
        {
            var itemService = CreateServiceWithSeededDb();
            var result = await itemService.RemoveItemById(999);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal("Item not found.", result.Message);
        }

        [Fact]
        public async Task TestRemoveItemByIdAsync_withEmptyDatabase()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var context = new DataContext(options);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<FrostedCornerWebAPI.AutoMapperProfile>());
            var mapper = config.CreateMapper();
            var itemService = new ItemService(mapper, context);
            var result = await itemService.RemoveItemById(1);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal("Item not found.", result.Message);
        }
    }
}