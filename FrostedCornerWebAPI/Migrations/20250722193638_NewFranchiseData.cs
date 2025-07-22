using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FrostedCornerWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class NewFranchiseData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Franchises",
                columns: table => new
                {
                    FranchiseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FranchiseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Franchises", x => x.FranchiseId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_FranchiseItemId",
                table: "Items",
                column: "FranchiseItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_OrderId",
                table: "Items",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_FranchiseItems_FranchiseId",
                table: "FranchiseItems",
                column: "FranchiseId");

            migrationBuilder.AddForeignKey(
                name: "FK_FranchiseItems_Franchises_FranchiseId",
                table: "FranchiseItems",
                column: "FranchiseId",
                principalTable: "Franchises",
                principalColumn: "FranchiseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_FranchiseItems_FranchiseItemId",
                table: "Items",
                column: "FranchiseItemId",
                principalTable: "FranchiseItems",
                principalColumn: "FranchiseItemId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Orders_OrderId",
                table: "Items",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FranchiseItems_Franchises_FranchiseId",
                table: "FranchiseItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_FranchiseItems_FranchiseItemId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Orders_OrderId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "Franchises");

            migrationBuilder.DropIndex(
                name: "IX_Items_FranchiseItemId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_OrderId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_FranchiseItems_FranchiseId",
                table: "FranchiseItems");

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
                    FranchiseItemId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
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
    }
}
