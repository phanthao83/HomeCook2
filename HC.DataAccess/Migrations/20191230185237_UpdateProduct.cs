using Microsoft.EntityFrameworkCore.Migrations;

namespace HC.DataAccess.Migrations
{
    public partial class UpdateProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Product",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "AvgRating",
                table: "Product",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Product",
                type: "float",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<double>(
                name: "AvgRating",
                table: "Product",
                type: "float",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
