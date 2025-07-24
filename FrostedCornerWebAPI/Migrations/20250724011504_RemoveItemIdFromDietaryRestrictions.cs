using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FrostedCornerWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemoveItemIdFromDietaryRestrictions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DietaryRestrictions_Items_ItemId",
                table: "DietaryRestrictions");

            migrationBuilder.DropIndex(
                name: "IX_DietaryRestrictions_ItemId",
                table: "DietaryRestrictions");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "DietaryRestrictions");

            migrationBuilder.CreateTable(
                name: "DietaryRestrictionItem",
                columns: table => new
                {
                    DietaryRestrictionsDietaryRestrictionId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietaryRestrictionItem", x => new { x.DietaryRestrictionsDietaryRestrictionId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_DietaryRestrictionItem_DietaryRestrictions_DietaryRestrictionsDietaryRestrictionId",
                        column: x => x.DietaryRestrictionsDietaryRestrictionId,
                        principalTable: "DietaryRestrictions",
                        principalColumn: "DietaryRestrictionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DietaryRestrictionItem_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DietaryRestrictionItem_ItemId",
                table: "DietaryRestrictionItem",
                column: "ItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DietaryRestrictionItem");

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "DietaryRestrictions",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "DietaryRestrictions",
                keyColumn: "DietaryRestrictionId",
                keyValue: 1,
                column: "ItemId",
                value: null);

            migrationBuilder.UpdateData(
                table: "DietaryRestrictions",
                keyColumn: "DietaryRestrictionId",
                keyValue: 2,
                column: "ItemId",
                value: null);

            migrationBuilder.UpdateData(
                table: "DietaryRestrictions",
                keyColumn: "DietaryRestrictionId",
                keyValue: 3,
                column: "ItemId",
                value: null);

            migrationBuilder.UpdateData(
                table: "DietaryRestrictions",
                keyColumn: "DietaryRestrictionId",
                keyValue: 4,
                column: "ItemId",
                value: null);

            migrationBuilder.UpdateData(
                table: "DietaryRestrictions",
                keyColumn: "DietaryRestrictionId",
                keyValue: 5,
                column: "ItemId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_DietaryRestrictions_ItemId",
                table: "DietaryRestrictions",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_DietaryRestrictions_Items_ItemId",
                table: "DietaryRestrictions",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId");
        }
    }
}
