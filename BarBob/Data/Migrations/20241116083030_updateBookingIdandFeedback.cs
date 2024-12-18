﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarBob.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateBookingIdandFeedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFeedbackGiven",
                table: "Bookings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFeedbackGiven",
                table: "Bookings");
        }
    }
}
