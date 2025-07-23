using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FrostedCornerWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddSuppliesOrderAndItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FranchiseId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "Items",
                type: "real",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "SuppliesOrders",
                columns: table => new
                {
                    SuppliesOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FranchiseId = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<float>(type: "real", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuppliesOrders", x => x.SuppliesOrderId);
                    table.ForeignKey(
                        name: "FK_SuppliesOrders_Franchises_FranchiseId",
                        column: x => x.FranchiseId,
                        principalTable: "Franchises",
                        principalColumn: "FranchiseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SuppliesItems",
                columns: table => new
                {
                    SuppliesItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SubTotal = table.Column<float>(type: "real", nullable: false),
                    SuppliesOrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuppliesItems", x => x.SuppliesItemId);
                    table.ForeignKey(
                        name: "FK_SuppliesItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SuppliesItems_SuppliesOrders_SuppliesOrderId",
                        column: x => x.SuppliesOrderId,
                        principalTable: "SuppliesOrders",
                        principalColumn: "SuppliesOrderId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SuppliesItems_ItemId",
                table: "SuppliesItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SuppliesItems_SuppliesOrderId",
                table: "SuppliesItems",
                column: "SuppliesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SuppliesOrders_FranchiseId",
                table: "SuppliesOrders",
                column: "FranchiseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SuppliesItems");

            migrationBuilder.DropTable(
                name: "SuppliesOrders");

            migrationBuilder.DropColumn(
                name: "FranchiseId",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "Price",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
