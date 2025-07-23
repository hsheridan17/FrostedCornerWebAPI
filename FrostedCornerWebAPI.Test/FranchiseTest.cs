using AutoMapper;
using FrostedCornerWebAPI.Data;
using FrostedCornerWebAPI.Models;
using FrostedCornerWebAPI.Services.FranchiseItemService;
using FrostedCornerWebAPI.Services.ItemService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrostedCornerWebAPI.Test
{
    public class FranchiseTest
    {
        private FranchiseItemService CreateServiceWithSeededDb()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var context = new DataContext(options);

            context.Franchises.AddRange(
        new Franchise { FranchiseId = 1, FranchiseName = "Franchise1", Address = "Address for Franchise1" },
        new Franchise { FranchiseId = 2, FranchiseName = "Franchise2", Address = "Address for Franchise2" },
        new Franchise { FranchiseId = 3, FranchiseName = "Franchise3", Address = "Address for Franchise3" },
        new Franchise { FranchiseId = 4, FranchiseName = "Franchise4", Address = "Address for Franchise4" }
    );

            // Seed Items with ItemType
            context.Items.AddRange(
                new Item { ItemId = 1, Name = "Item1", Price = 12.99f, Description = "Description for Item1", ItemType = ItemType.Food },
                new Item { ItemId = 2, Name = "Item2", Price = 10.99f, Description = "Description for Item2", ItemType = ItemType.Food },
                new Item { ItemId = 3, Name = "Item3", Price = 15.99f, Description = "Description for Item3", ItemType = ItemType.Food },
                new Item { ItemId = 4, Name = "Item4", Price = 20.99f, Description = "Description for Item4", ItemType = ItemType.Supply }
            );

            // Seed DietaryRestrictions for items
            context.DietaryRestrictions.AddRange(
                new DietaryRestriction { DietaryRestrictionId = 1, Name = "Nut-Free"},
                new DietaryRestriction { DietaryRestrictionId = 2, Name = "Gluten-Free"}
            );

            // Seed FranchiseItems with more variety
            context.FranchiseItems.AddRange(
                new FranchiseItem
                {
                    FranchiseItemId = 1,
                    FranchiseId = 1,
                    ItemId = 1,
                    CustomColor = "",
                    CustomPrice = 12.99f
                },
                new FranchiseItem
                {
                    FranchiseItemId = 2,
                    FranchiseId = 1,
                    ItemId = 2,
                    CustomColor = "Blue",
                    CustomPrice = 10.99f
                },
                new FranchiseItem
                {
                    FranchiseItemId = 3,
                    FranchiseId = 3,
                    ItemId = 1,
                    CustomColor = "Purple",
                    CustomPrice = 12.99f
                },
                new FranchiseItem
                {
                    FranchiseItemId = 4,
                    FranchiseId = 2,
                    ItemId = 1,
                    CustomColor = "",
                    CustomPrice = 12.99f
                },
                new FranchiseItem
                {
                    FranchiseItemId = 5,
                    FranchiseId = 2,
                    ItemId = 3,
                    CustomColor = "Green",
                    CustomPrice = 15.99f
                }
            );
            context.SaveChanges();

            var config = new MapperConfiguration(cfg => cfg.AddProfile<FrostedCornerWebAPI.AutoMapperProfile>());
            var mapper = config.CreateMapper();

            return new FranchiseItemService(mapper, context);
        }

        [Fact]
        public async Task TestGetAllFranchisesAsync()
        {
            // Arrange
            var franchiseService = CreateServiceWithSeededDb();
            // Act
            var result = await franchiseService.GetAllFranchises();
            // Assert
            Assert.NotNull(result.Data);
            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task TestGetFranchiseByIdAsync_ValidId()
        {
            // Arrange
            var franchiseService = CreateServiceWithSeededDb();
            // Act
            var result = await franchiseService.GetFranchiseById(1);
            // Assert
            Assert.NotNull(result.Data);
            Assert.Equal("Franchise1", result.Data.FranchiseName);
        }

        [Fact]
        public async Task TestGetFranchiseByIdAsync_InvalidId()
        {
            // Arrange
            var franchiseService = CreateServiceWithSeededDb();
            // Act
            var result = await franchiseService.GetFranchiseById(999);
            // Assert
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal("Franchise not found.", result.Message);
        }



        //[Fact]
        //public async Task TestAddFranchise()
        //{
        //    // Arrange
        //    var franchiseService = CreateServiceWithSeededDb();
        //    var newFranchise = new FrostedCornerWebAPI.Dtos.FranchiseItem.AddFranchiseDto
        //    {
        //        FranchiseName = "New Franchise",
        //        Address = "New Address"
        //    };
        //    // Act
        //    var result = await franchiseService.AddFranchise(newFranchise);
        //    // Assert
        //    Assert.NotNull(result.Data);
        //    //Assert.Single(result.Data);
        //    Assert.Equal("New Franchise", result.Data.First().FranchiseName);
        //}

        [Fact]
        public async Task TestAddFranchiseItem_WithValidFranIdItemId()
        {
            // Arrange
            var franchiseService = CreateServiceWithSeededDb();
            var FranchiseId = 1;
            var ItemId = 3;
            // Act
            var result = await franchiseService.AddFranchiseItem(FranchiseId, ItemId);
            // Assert
            Assert.NotNull(result.Data);
            Assert.Equal(3, result.Data.FranchiseItems.Count);
            Assert.Contains(result.Data.FranchiseItems, fi => fi.Item.Id == ItemId);
            Assert.Equal("Item3", result.Data.FranchiseItems.First(fi => fi.Item.Id == ItemId).Item.Name);
        }

        [Fact]
        public async Task TestAddFranchiseItem_WithInvalidFranIdItemId()
        {
            // Arrange
            var franchiseService = CreateServiceWithSeededDb();
            var FranchiseId = 999; // Invalid FranchiseId
            var ItemId = 999; // Invalid ItemId
                              // Act
            var result = await franchiseService.AddFranchiseItem(FranchiseId, ItemId);
            // Assert
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal("Order not found.", result.Message);
        }

        [Fact]
        public async Task TestAddFranchiseItem_WithInvalidFranId_ValidItemId()
        {
            // Arrange
            var franchiseService = CreateServiceWithSeededDb();
            var FranchiseId = 999; // Invalid FranchiseId
            var ItemId = 2; // Invalid ItemId
                            // Act
            var result = await franchiseService.AddFranchiseItem(FranchiseId, ItemId);
            // Assert
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal("Order not found.", result.Message);
        }

        [Fact]
        public async Task TestAddFranchiseItem_WithValidFranId_InvalidItemId()
        {
            // Arrange
            var franchiseService = CreateServiceWithSeededDb();
            var FranchiseId = 4;
            var ItemId = 999; // Invalid ItemId
                              // Act
            var result = await franchiseService.AddFranchiseItem(FranchiseId, ItemId);
            // Assert
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal("Item not found.", result.Message);
        }

        [Fact]
        public async Task TestEditFranchiseItem_WithValidId()
        {
            // Arrange
            var franchiseService = CreateServiceWithSeededDb();

            var NewFranchiseItem = new FrostedCornerWebAPI.Dtos.FranchiseItem.EditFranchiseItemDto
            {
                ItemId = 2,
                FranchiseId = 1,
                CustomColor = "Red",
                CustomPrice = 9.99f
            };
            // Act
            var result = await franchiseService.EditFranchiseItem(NewFranchiseItem);
            // Assert
            Assert.NotNull(result.Data);
            Assert.Equal("Red", result.Data.CustomColor);
            Assert.Equal(9.99f, result.Data.CustomPrice);
            Assert.Equal(2, result.Data.Item.Id);
            Assert.Equal("Item2", result.Data.Item.Name);
        }

        [Fact]
        public async Task TestEditFranchiseItem_WithInvalidId()
        {
            // Arrange
            var franchiseService = CreateServiceWithSeededDb();
            var NewFranchiseItem = new FrostedCornerWebAPI.Dtos.FranchiseItem.EditFranchiseItemDto
            {
                ItemId = 999, // Invalid ItemId
                CustomColor = "Red",
                CustomPrice = 9.99f
            };
            // Act
            var result = await franchiseService.EditFranchiseItem(NewFranchiseItem);
            // Assert
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal("Item not found.", result.Message);
        }

        [Fact]
        public async Task TestEditFranchiseItem_WithoutColor()
        {
            // Arrange
            var franchiseService = CreateServiceWithSeededDb();
            var NewFranchiseItem = new FrostedCornerWebAPI.Dtos.FranchiseItem.EditFranchiseItemDto
            {
                ItemId = 1,
                FranchiseId = 1,
                CustomPrice = 9.99f
            };
            // Act
            var result = await franchiseService.EditFranchiseItem(NewFranchiseItem);
            // Assert
            Assert.NotNull(result.Data);
            Assert.Equal("", result.Data.CustomColor);
            Assert.Equal(9.99f, result.Data.CustomPrice);
            Assert.Equal(1, result.Data.Item.Id);
            Assert.Equal("Item1", result.Data.Item.Name);
        }

        [Fact]
        public async Task TestEditFranchiseItem_WithoutPrice()
        {
            // Arrange
            var franchiseService = CreateServiceWithSeededDb();
            var NewFranchiseItem = new FrostedCornerWebAPI.Dtos.FranchiseItem.EditFranchiseItemDto
            {
                ItemId = 1,
                FranchiseId = 2,
                CustomColor = "Purple"
            };
            // Act
            var result = await franchiseService.EditFranchiseItem(NewFranchiseItem);
            // Assert
            Assert.NotNull(result.Data);
            Assert.Equal("Purple", result.Data.CustomColor);
            Assert.Equal(0, result.Data.CustomPrice);
            Assert.Equal(1, result.Data.Item.Id);
            Assert.Equal("Item1", result.Data.Item.Name);
        }

        //[Fact]
        //public async Task TestAddItemToAllFranchises()
        //{
        //    // Arrange
        //    var franchiseService = CreateServiceWithSeededDb();
        //    var newFranchiseItem = 4;
        //    // Act
        //    var result = await franchiseService.AddItemToAllFranchises(newFranchiseItem);
        //    // Assert
        //    Assert.NotNull(result.Data);
        //    Assert.Equal(4, result.Data.Count);
        //    Assert.Equal(newFranchiseItem, result.Data.First().FranchiseItems.First().Item.Id);
        //    Assert.Contains(result.Data, f => f.FranchiseItems.Any(fi => fi.Item.Id == newFranchiseItem));
        //    Assert.Contains(result.Data, f => f.FranchiseItems.Any(fi => fi.Item.Name == "Item4"));
        //    Assert.Contains(result.Data, f => f.FranchiseItems.Any(fi => fi.CustomColor == "Green"));
        //    Assert.Contains(result.Data, f => f.FranchiseItems.Any(fi => fi.CustomPrice == 20.99f));
        //}
    }
}
