using Microsoft.EntityFrameworkCore.Migrations;

namespace OPM.Infrastructure.Migrations
{
    public partial class EntProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EntityID",
                table: "EntityProfile",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EntityName",
                table: "EntityProfile",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntityID",
                table: "EntityProfile");

            migrationBuilder.DropColumn(
                name: "EntityName",
                table: "EntityProfile");
        }
    }
}
