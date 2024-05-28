using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarBob.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReCrDatab : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Bills_BillId",
                table: "Payment");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "BookingRequests");

            migrationBuilder.DropTable(
                name: "Deposits");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Payment_BillId",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "BillId",
                table: "Payment");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Tables",
                newName: "TableImg");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Payment",
                newName: "BookingId");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Payment",
                newName: "Expire_date");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Tables",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TableTypeId",
                table: "Tables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Credit_card_no",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "First_name",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Last_name",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckinDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckinTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Guests = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TableId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TableTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Table_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tables_TableTypeId",
                table: "Tables",
                column: "TableTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_BookingId",
                table: "Payment",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TableId",
                table: "Bookings",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Bookings_BookingId",
                table: "Payment",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_TableTypes_TableTypeId",
                table: "Tables",
                column: "TableTypeId",
                principalTable: "TableTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Bookings_BookingId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Tables_TableTypes_TableTypeId",
                table: "Tables");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "TableTypes");

            migrationBuilder.DropIndex(
                name: "IX_Tables_TableTypeId",
                table: "Tables");

            migrationBuilder.DropIndex(
                name: "IX_Payment_BookingId",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "TableTypeId",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "Credit_card_no",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "First_name",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "Last_name",
                table: "Payment");

            migrationBuilder.RenameColumn(
                name: "TableImg",
                table: "Tables",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Expire_date",
                table: "Payment",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "BookingId",
                table: "Payment",
                newName: "Status");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Time",
                table: "Tables",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "BillId",
                table: "Payment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BookingRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Guests = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingRequests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingRequests_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Deposits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cost = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cost = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingRequestId = table.Column<int>(type: "int", nullable: false),
                    DepositId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_BookingRequests_BookingRequestId",
                        column: x => x.BookingRequestId,
                        principalTable: "BookingRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bills_Deposits_DepositId",
                        column: x => x.DepositId,
                        principalTable: "Deposits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bills_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payment_BillId",
                table: "Payment",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_BookingRequestId",
                table: "Bills",
                column: "BookingRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_DepositId",
                table: "Bills",
                column: "DepositId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_ServiceId",
                table: "Bills",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingRequests_TableId",
                table: "BookingRequests",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingRequests_UserId",
                table: "BookingRequests",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Bills_BillId",
                table: "Payment",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
