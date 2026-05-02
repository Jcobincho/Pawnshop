using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pawnshop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkplaceCoordinates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Workplaces",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Workplaces",
                type: "double precision",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Workplaces");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Workplaces");
        }
    }
}
