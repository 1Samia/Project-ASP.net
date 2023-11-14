using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotels.Migrations
{
    /// <inheritdoc />
    public partial class addEmailPhone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "hotel",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Phone",
                table: "hotel",
                type: "int",
                maxLength: 20,
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "hotel");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "hotel");
        }
    }
}
