using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FrostedCornerWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemoveInheritance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_FranchiseItems_FranchiseItemId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Orders_OrderId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_FranchiseItemId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_OrderId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "FranchiseItemId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "OrderItemId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "SubTotal",
                table: "Items");

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    FranchiseItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SubTotal = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItems_FranchiseItems_FranchiseItemId",
                        column: x => x.FranchiseItemId,
                        principalTable: "FranchiseItems",
                        principalColumn: "FranchiseItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_FranchiseItemId",
                table: "OrderItems",
                column: "FranchiseItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Items",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FranchiseItemId",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderItemId",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "SubTotal",
                table: "Items",
                type: "real",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_FranchiseItemId",
                table: "Items",
                column: "FranchiseItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_OrderId",
                table: "Items",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_FranchiseItems_FranchiseItemId",
                table: "Items",
                column: "FranchiseItemId",
                principalTable: "FranchiseItems",
                principalColumn: "FranchiseItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Orders_OrderId",
                table: "Items",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
