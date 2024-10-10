using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarBob.Data.Migrations
{
    /// <inheritdoc />
    public partial class addCountBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "First_name",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "Last_name",
                table: "Payment");

            migrationBuilder.RenameColumn(
                name: "Credit_card_no",
                table: "Payment",
                newName: "TransactionId");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Payment",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Count",
                table: "Bookings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "Payment",
                newName: "Credit_card_no");

            migrationBuilder.AddColumn<string>(
                name: "First_name",
                table: "Payment",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Last_name",
                table: "Payment",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
