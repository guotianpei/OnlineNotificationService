using Microsoft.EntityFrameworkCore.Migrations;

namespace OPM.Infrastructure.Migrations
{
    public partial class AddedForeignKeyOnTableProfileComChannelEntityID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileComChannel_EntityProfile_EntityID",
                table: "ProfileComChannel");

            migrationBuilder.AlterColumn<string>(
                name: "EntityID",
                table: "ProfileComChannel",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "EntityID",
                table: "EntityProfile",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_EntityProfile_EntityID",
                table: "EntityProfile",
                column: "EntityID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileComChannel_EntityProfile_EntityID",
                table: "ProfileComChannel",
                column: "EntityID",
                principalTable: "EntityProfile",
                principalColumn: "EntityID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileComChannel_EntityProfile_EntityID",
                table: "ProfileComChannel");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_EntityProfile_EntityID",
                table: "EntityProfile");

            migrationBuilder.AlterColumn<int>(
                name: "EntityID",
                table: "ProfileComChannel",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "EntityID",
                table: "EntityProfile",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileComChannel_EntityProfile_EntityID",
                table: "ProfileComChannel",
                column: "EntityID",
                principalTable: "EntityProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
