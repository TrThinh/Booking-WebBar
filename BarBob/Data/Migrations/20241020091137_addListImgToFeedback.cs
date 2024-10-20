using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarBob.Data.Migrations
{
    /// <inheritdoc />
    public partial class addListImgToFeedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image1",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "Image2",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "Image3",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "Image4",
                table: "Feedbacks");

            migrationBuilder.RenameColumn(
                name: "Image5",
                table: "Feedbacks",
                newName: "Images");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Images",
                table: "Feedbacks",
                newName: "Image5");

            migrationBuilder.AddColumn<string>(
                name: "Image1",
                table: "Feedbacks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image2",
                table: "Feedbacks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image3",
                table: "Feedbacks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image4",
                table: "Feedbacks",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
