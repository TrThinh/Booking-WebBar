using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarBob.Data.Migrations
{
    /// <inheritdoc />
    public partial class bookingrq_bill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TableId",
                table: "BookingRequests",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "BookingRequestId",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BookingRequests_TableId",
                table: "BookingRequests",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_BookingRequestId",
                table: "Bills",
                column: "BookingRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_BookingRequests_BookingRequestId",
                table: "Bills",
                column: "BookingRequestId",
                principalTable: "BookingRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingRequests_Tables_TableId",
                table: "BookingRequests",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_BookingRequests_BookingRequestId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingRequests_Tables_TableId",
                table: "BookingRequests");

            migrationBuilder.DropIndex(
                name: "IX_BookingRequests_TableId",
                table: "BookingRequests");

            migrationBuilder.DropIndex(
                name: "IX_Bills_BookingRequestId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "BookingRequestId",
                table: "Bills");

            migrationBuilder.AlterColumn<string>(
                name: "TableId",
                table: "BookingRequests",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
