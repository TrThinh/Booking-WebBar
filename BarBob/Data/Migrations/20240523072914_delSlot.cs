using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarBob.Data.Migrations
{
    /// <inheritdoc />
    public partial class delSlot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingRequests_Slots_SlotId",
                table: "BookingRequests");

            migrationBuilder.DropTable(
                name: "Slots");

            migrationBuilder.DropIndex(
                name: "IX_BookingRequests_SlotId",
                table: "BookingRequests");

            migrationBuilder.DropColumn(
                name: "SlotId",
                table: "BookingRequests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SlotId",
                table: "BookingRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Slots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TablePosition = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slots", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingRequests_SlotId",
                table: "BookingRequests",
                column: "SlotId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingRequests_Slots_SlotId",
                table: "BookingRequests",
                column: "SlotId",
                principalTable: "Slots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
