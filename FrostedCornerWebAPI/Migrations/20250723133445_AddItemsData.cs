using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FrostedCornerWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddItemsData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DietaryRestriction_Items_ItemId",
                table: "DietaryRestriction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DietaryRestriction",
                table: "DietaryRestriction");

            migrationBuilder.RenameTable(
                name: "DietaryRestriction",
                newName: "DietaryRestrictions");

            migrationBuilder.RenameIndex(
                name: "IX_DietaryRestriction_ItemId",
                table: "DietaryRestrictions",
                newName: "IX_DietaryRestrictions_ItemId");

            migrationBuilder.AddColumn<string>(
                name: "ItemName",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ItemType",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DietaryRestrictions",
                table: "DietaryRestrictions",
                column: "DietaryRestrictionId");

            migrationBuilder.InsertData(
                table: "DietaryRestrictions",
                columns: new[] { "DietaryRestrictionId", "ItemId", "Name" },
                values: new object[,]
                {
                    { 1, null, "Vegan" },
                    { 2, null, "Vegetarian" },
                    { 3, null, "Gluten-Free" },
                    { 4, null, "Nut-Free" },
                    { 5, null, "Dairy-Free" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_DietaryRestrictions_Items_ItemId",
                table: "DietaryRestrictions",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DietaryRestrictions_Items_ItemId",
                table: "DietaryRestrictions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DietaryRestrictions",
                table: "DietaryRestrictions");

            migrationBuilder.DeleteData(
                table: "DietaryRestrictions",
                keyColumn: "DietaryRestrictionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DietaryRestrictions",
                keyColumn: "DietaryRestrictionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DietaryRestrictions",
                keyColumn: "DietaryRestrictionId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DietaryRestrictions",
                keyColumn: "DietaryRestrictionId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DietaryRestrictions",
                keyColumn: "DietaryRestrictionId",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "ItemName",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ItemType",
                table: "Items");

            migrationBuilder.RenameTable(
                name: "DietaryRestrictions",
                newName: "DietaryRestriction");

            migrationBuilder.RenameIndex(
                name: "IX_DietaryRestrictions_ItemId",
                table: "DietaryRestriction",
                newName: "IX_DietaryRestriction_ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DietaryRestriction",
                table: "DietaryRestriction",
                column: "DietaryRestrictionId");

            migrationBuilder.AddForeignKey(
                name: "FK_DietaryRestriction_Items_ItemId",
                table: "DietaryRestriction",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId");
        }
    }
}
