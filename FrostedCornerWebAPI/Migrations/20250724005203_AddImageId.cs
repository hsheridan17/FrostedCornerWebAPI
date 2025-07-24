using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FrostedCornerWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddImageId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageId",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Items");
        }
    }
}
