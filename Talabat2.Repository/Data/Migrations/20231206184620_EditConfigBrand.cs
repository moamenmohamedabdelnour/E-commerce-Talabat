using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Talabat2.Repository.Data.Migrations
{
    public partial class EditConfigBrand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProductBrands",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryMethodId",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int?");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBrands_Name",
                table: "ProductBrands",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductBrands_Name",
                table: "ProductBrands");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProductBrands",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryMethodId",
                table: "Orders",
                type: "int?",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
