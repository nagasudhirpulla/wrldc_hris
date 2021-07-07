using Microsoft.EntityFrameworkCore.Migrations;

namespace WrldcHrIs.Infra.Migrations
{
    public partial class canteenOrderPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FoodItemName",
                table: "CanteenOrders",
                type: "character varying(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<string>(
                name: "FoodItemDescription",
                table: "CanteenOrders",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "FoodItemUnitPrice",
                table: "CanteenOrders",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FoodItemDescription",
                table: "CanteenOrders");

            migrationBuilder.DropColumn(
                name: "FoodItemUnitPrice",
                table: "CanteenOrders");

            migrationBuilder.AlterColumn<string>(
                name: "FoodItemName",
                table: "CanteenOrders",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(250)",
                oldMaxLength: 250);
        }
    }
}
