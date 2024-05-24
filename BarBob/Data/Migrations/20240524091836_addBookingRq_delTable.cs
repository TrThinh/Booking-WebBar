using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarBob.Data.Migrations
{
    /// <inheritdoc />
    public partial class addBookingRq_delTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Tables");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "BookingRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Guests",
                table: "BookingRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "BookingRequests");

            migrationBuilder.DropColumn(
                name: "Guests",
                table: "BookingRequests");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Tables",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
